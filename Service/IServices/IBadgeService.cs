using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IBadgeService
    {
        Task<Badge> AddBadge(Badge g);
        Task<Badge> UpdateBadge(Badge g);
        Task<Badge> DeleteBadge(int id);
        Task<IEnumerable<Badge>> GetAllBadges();
        Task<Badge> GetBadgeById(int id);
    }
}
