using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IContentRepository : IGenericRepository<Content>
    {
        Task<IEnumerable<Content>> GetContentsByLessonId(int lessonId);
        Task<bool> IsValidContentId(int id);

    }
}
