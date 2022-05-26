using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IContentService
    {
        Task<Content> AddContent(Content g);
        Task<Content> UpdateContent(Content g);
        Task<Content> DeleteContent(int id);
        Task<IEnumerable<Content>> GetAllContents();
        Task<Content> GetContentById(int id);
    }
}
