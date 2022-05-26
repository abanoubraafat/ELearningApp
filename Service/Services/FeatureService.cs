using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository iFeatureRepository;

        public FeatureService(IFeatureRepository _iFeatureRepository)
        {
            iFeatureRepository = _iFeatureRepository;
        }
        public async Task<Feature> AddFeature(Feature g)
        {
            return await iFeatureRepository.Add(g);
        }

        public async Task<Feature> DeleteFeature(int id)
        {
            return await iFeatureRepository.Delete(id);
        }

        public async Task<IEnumerable<Feature>> GetAllFeatures()
        {
            return await iFeatureRepository.GetAll().ToListAsync();
        }

        public async Task<Feature> GetFeatureById(int id)
        {
            return await iFeatureRepository.GetById(id);
        }

        public async Task<Feature> UpdateFeature(Feature g)
        {
            return await iFeatureRepository.Update(g);
        }
    }
}
