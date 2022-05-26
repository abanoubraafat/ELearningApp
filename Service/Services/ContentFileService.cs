using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository iContentRepositroy;
        public ContentService(IContentRepository _iContentRepository)
        {
            iContentRepositroy = _iContentRepository;
        }
        public async Task<Content> AddContent(Content f)
        {
            return await iContentRepositroy.Add(f);
        }

        public async Task<Content> DeleteContent(int id)
        {
            return await iContentRepositroy.Delete(id);
        }

        public async Task<IEnumerable<Content>> GetAllContents()
        {
            return await iContentRepositroy.GetAll().ToListAsync();
        }

        public async Task<Content> GetContentById(int id)
        {
            return await iContentRepositroy.GetById(id);
        }

        public async Task<Content> UpdateContent(Content f)
        {
            return await iContentRepositroy.Update(f);
        }
    }
}
