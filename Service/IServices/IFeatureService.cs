using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IFeatureService
    {
        Task<Feature> AddFeature(Feature g);
        Task<Feature> UpdateFeature(Feature g);
        Task<Feature> DeleteFeature(int id);
        Task<IEnumerable<Feature>> GetAllFeatures();
        Task<Feature> GetFeatureById(int id);
    }
}
