using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository iResourceRepository;

        public ResourceService(IResourceRepository _iResourceRepository)
        {
            iResourceRepository = _iResourceRepository;
        }
        public async Task<Resource> AddResource(Resource g)
        {
            return await iResourceRepository.Add(g);
        }

        public async Task<Resource> DeleteResource(int id)
        {
            return await iResourceRepository.Delete(id);
        }

        public async Task<IEnumerable<Resource>> GetAllResources()
        {
            return await iResourceRepository.GetAll().ToListAsync();
        }

        public async Task<Resource> GetResourceById(int id)
        {
            return await iResourceRepository.GetById(id);
        }

        public async Task<Resource> UpdateResource(Resource g)
        {
            return await iResourceRepository.Update(g);
        }
    }
}
