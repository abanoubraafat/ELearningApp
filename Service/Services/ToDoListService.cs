using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository iToDoListRepository;
        public ToDoListService(IToDoListRepository _toDoListRepository)
        {
            this.iToDoListRepository = _toDoListRepository;
        }
        public async Task<ToDoList> AddToDoList(ToDoList l)
        {
            return await iToDoListRepository.Add(l);
        }

        public async Task<ToDoList> DeleteToDoList(int id)
        {
            return await iToDoListRepository.Delete(id);
        }

        public async Task<IEnumerable<ToDoList>> GetAllToDoLists()
        {
            return await iToDoListRepository.GetAll().ToListAsync();
        }

        public async Task<ToDoList> GetToDoListById(int id)
        {
            return await iToDoListRepository.GetById(id);
        }

        public async Task<ToDoList> UpdateToDoList(ToDoList l)
        {
            return await iToDoListRepository.Update(l);
        }
    }
}
