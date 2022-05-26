using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public LessonRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Lesson>> GetLessonsByCourseId(int courseId)
        {
            return await unitOfWork.Context.Lessons
                .Where(l => l.CourseId == courseId)
                .ToListAsync();
        }
        public async Task<bool> IsValidLessonId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        //public IQueryable<Lesson> GetByIdWithContent(int id)
        //{
        //    return unitOfWork.Context.Lessons.Where(l => l.Id == id).Include(l => l.Contents);
        //}

        //public IQueryable<Lesson> GetByIdWithQuestions(int id)
        //{
        //    return unitOfWork.Context.Lessons.Where(l => l.Id == id).Include(l => l.Questions);
        //}

        //public IQueryable<Lesson> GetByIdWithNotes(int id)
        //{
        //    return unitOfWork.Context.Lessons.Where(l => l.Id == id).Include(l => l.Notes);
        //}
    }
}
