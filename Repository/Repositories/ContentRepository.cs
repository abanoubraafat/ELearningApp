using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public ContentRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Content>> GetContentsByLessonId(int lessonId)
        {
            return await unitOfWork.Context.Content
                .Where(c => c.LessonId == lessonId)
                .ToListAsync();
        }
        public async Task<bool> IsValidContentId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }
    }
}
