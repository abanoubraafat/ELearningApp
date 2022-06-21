using Domain.DTOs;
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
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var loginRespone = new LoginResponse();

            var user = await unitOfWork.Context.Users.SingleOrDefaultAsync(u => u.EmailAddress == loginRequest.EmailAddress);
            if (user == null)
                return null;
            bool isValidPassword = VerifyPassword(loginRequest.Password, user.Password);
            if (!isValidPassword)
                return null;

            var jwtSecurityToken = await CreateJwtToken(user);
            loginRespone.IsAuthenticated = true;
            loginRespone.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            loginRespone.Email = user.EmailAddress;
            loginRespone.ExpiresOn = jwtSecurityToken.ValidTo;
            loginRespone.Role = user.Role;

            return loginRespone;
        }
    }
}
