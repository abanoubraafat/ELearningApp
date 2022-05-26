using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;

namespace ELearning_App.Repository.Repositories
{
    public class ResourceRepository : GenericRepository<Resource>, IResourceRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public ResourceRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<bool> IsValidResourceId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }
    }
}
