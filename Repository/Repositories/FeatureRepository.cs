using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class FeatureRepository : GenericRepository<Feature>, IFeatureRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public FeatureRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Feature>> GetFeaturesByStudentId(int studentId)
        {
            return await unitOfWork.Context.Features
                .Where(f => f.StudentId == studentId)
                .ToListAsync();
        }
        public async Task<bool> IsValidFeatureId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }
    }
}
