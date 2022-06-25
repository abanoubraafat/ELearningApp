using Domain.DTOs;
using Domain.Entities;
using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace ELearning_App.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private IUnitOfWork unitOfWork { get; }
        private readonly JWT _jwt;
        public UserRepository(IUnitOfWork _unitOfWork, IOptions<JWT> jwt) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
            _jwt = jwt.Value;
        }
        public async Task<bool> IsValidUserId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        //public async Task<User> Login(string email, string password)
        //{
        //    var user = await unitOfWork.Context.Users.SingleOrDefaultAsync(u => u.EmailAddress == email);
        //    if (user == null)
        //        return null;
        //    bool isValidPassword = VerifyPassword(password, user.Password);
        //    if(!isValidPassword)
        //        return null;
        //    else
        //        return user;
        //}

        public async Task<bool> IsNotAvailableUserEmail(string email)
        {
            return await unitOfWork.Context.Users.AnyAsync(u => u.EmailAddress.Equals(email));
            //if true means un valid
        }

        public string CreatePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await unitOfWork.Context.Users.SingleOrDefaultAsync(u => u.EmailAddress == email);
        }

        //public IQueryable<LoginInfo> GetByIdWithToDoLists(int id)
        //{
        //    return unitOfWork.Context.LoginInfos.Where(l => l.Id == id).Include(l => l.ToDoLists);
        //}
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
                new Claim("role", user.Role),
                new Claim("id", user.Id.ToString())
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var loginResponse = new LoginResponse();

            var user = await unitOfWork.Context.Users.SingleOrDefaultAsync(u => u.EmailAddress == loginRequest.EmailAddress);
            if (user == null)
                return null;
            bool isValidPassword = VerifyPassword(loginRequest.Password, user.Password);
            if (!isValidPassword)
                return null;

            var jwtSecurityToken = await CreateJwtToken(user);
            loginResponse.IsAuthenticated = true;
            loginResponse.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            loginResponse.Email = user.EmailAddress;
            loginResponse.ExpiresOn = jwtSecurityToken.ValidTo;
            loginResponse.Role = user.Role;
            if(user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                loginResponse.RefreshToken = activeRefreshToken.Token;
                loginResponse.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;

            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                loginResponse.RefreshToken = refreshToken.Token;
                loginResponse.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await Update(user);
            }
            return loginResponse;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }

        public async Task<LoginResponse> RefreshTokenAsync(string token)
        {
            var loginResponse = new LoginResponse();

            var user = await unitOfWork.Context.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                loginResponse.Message = "Invalid token";
                return loginResponse;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                loginResponse.Message = "Inactive token";
                return loginResponse;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await Update(user);

            var jwtToken = await CreateJwtToken(user);
            loginResponse.IsAuthenticated = true;
            loginResponse.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            loginResponse.Email = user.EmailAddress;
            loginResponse.Role = user.Role;
            loginResponse.RefreshToken = newRefreshToken.Token;
            loginResponse.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
            loginResponse.ExpiresOn = jwtToken.ValidTo;
            return loginResponse;
        }
        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await unitOfWork.Context.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;

            await Update(user);

            return true;
        }

        public async Task<User> LoginTest(LoginRequest loginRequest)
        {
            var user = await unitOfWork.Context.Users.SingleOrDefaultAsync(u => u.EmailAddress == loginRequest.EmailAddress);
            if (user == null)
                return null;
            bool isValidPassword = VerifyPassword(loginRequest.Password, user.Password);
            if (!isValidPassword)
                return null;
            return user;
        }
    }
}
