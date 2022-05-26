using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IFeatureRepository : IGenericRepository<Feature>
    {
        Task<IEnumerable<Feature>> GetFeaturesByStudentId(int studentId);
        Task<bool> IsValidFeatureId(int id);

    }
}
