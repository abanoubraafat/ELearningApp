using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IBadgeRepository : IGenericRepository<Badge>
    {
        Task<Badge> GetBadgeByAssignmentAnswerId(int assignmentAnswerId);
        Task<bool> IsValidBadgeId(int id);

    }
}
