using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        //IQueryable<Lesson> GetByIdWithContent(int id);
        //IQueryable<Lesson> GetByIdWithQuestions(int id);
        //IQueryable<Lesson> GetByIdWithNotes(int id);
        Task<IEnumerable<Lesson>> GetLessonsByCourseId(int courseId);
        Task<bool> IsValidLessonId(int id);


    }
}
