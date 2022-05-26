using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class BadgeRepository : GenericRepository<Badge>, IBadgeRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public BadgeRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<Badge> GetBadgeByAssignmentAnswerId(int assignmentAnswerId)
        {
            return await unitOfWork.Context.Badges
                .FirstAsync(b => b.AssignmentAnswerId == assignmentAnswerId);
        }
        public async Task<bool> IsValidBadgeId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }
    }
}