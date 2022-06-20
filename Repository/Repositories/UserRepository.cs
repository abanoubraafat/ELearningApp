using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public UserRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<bool> IsValidUserId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await unitOfWork.Context.Users.SingleOrDefaultAsync(u => u.EmailAddress == email);
            if (user == null)
                return null;
            bool isValidPassword = VerifyPassword(password, user.Password);
            if(!isValidPassword)
                return null;
            else
                return user;
        }

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
    }
}
