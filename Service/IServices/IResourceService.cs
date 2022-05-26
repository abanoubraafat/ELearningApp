using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IResourceService
    {
        Task<Resource> AddResource(Resource g);
        Task<Resource> UpdateResource(Resource g);
        Task<Resource> DeleteResource(int id);
        Task<IEnumerable<Resource>> GetAllResources();
        Task<Resource> GetResourceById(int id);
    }
}
