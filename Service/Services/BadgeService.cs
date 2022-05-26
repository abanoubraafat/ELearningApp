using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly IBadgeRepository iBadgeRepository;

        public BadgeService(IBadgeRepository _iBadgeRepository)
        {
            iBadgeRepository = _iBadgeRepository;
        }
        public async Task<Badge> AddBadge(Badge g)
        {
            return await iBadgeRepository.Add(g);
        }

        public async Task<Badge> DeleteBadge(int id)
        {
            return await iBadgeRepository.Delete(id);
        }

        public async Task<IEnumerable<Badge>> GetAllBadges()
        {
            return await iBadgeRepository.GetAll().ToListAsync();
        }

        public async Task<Badge> GetBadgeById(int id)
        {
            return await iBadgeRepository.GetById(id);
        }

        public async Task<Badge> UpdateBadge(Badge g)
        {
            return await iBadgeRepository.Update(g);
        }
    }
}
