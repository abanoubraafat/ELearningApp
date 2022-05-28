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
            return await unitOfWork.Context.Users.SingleOrDefaultAsync(u => u.EmailAddress == email && u.Password == password);
        }

        //public IQueryable<LoginInfo> GetByIdWithToDoLists(int id)
        //{
        //    return unitOfWork.Context.LoginInfos.Where(l => l.Id == id).Include(l => l.ToDoLists);
        //}
    }
}
