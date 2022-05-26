using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class ToDoListRepository : GenericRepository<ToDoList>, IToDoListRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public ToDoListRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<ToDoList>> GetToDoListsByUserId(int userId)
        {
            return await unitOfWork.Context.ToDoLists
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
        public async Task<bool> IsValidToDoListId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }
    }
}
