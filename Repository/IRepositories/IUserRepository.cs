using Domain.DTOs;
using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        //IQueryable<User> GetByIdWithToDoLists(int id);
        Task<bool> IsValidUserId(int id);
        Task<bool> IsNotAvailableUserEmail(string email);
        //Task<User> Login(string email, string password);
        string CreatePasswordHash(string password);
        bool VerifyPassword(string password, string hashedPassword);
        Task<User> GetByEmailAsync(string email);
        Task<LoginResponse> Login(LoginRequest loginRequest);

    }
}
