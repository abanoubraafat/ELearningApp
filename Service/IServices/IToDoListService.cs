using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IToDoListService
    {
        Task<ToDoList> AddToDoList(ToDoList g);
        Task<ToDoList> UpdateToDoList(ToDoList g);
        Task<ToDoList> DeleteToDoList(int id);
        Task<IEnumerable<ToDoList>> GetAllToDoLists();
        Task<ToDoList> GetToDoListById(int id);
    }
}
