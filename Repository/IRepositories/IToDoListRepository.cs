using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IToDoListRepository : IGenericRepository<ToDoList>
    {
        Task<IEnumerable<ToDoList>> GetToDoListsByUserId(int userId);
        Task<bool> IsValidToDoListId(int id);

    }
}
