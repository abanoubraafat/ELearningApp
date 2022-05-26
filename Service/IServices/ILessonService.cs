using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface ILessonService
    {
        Task<Lesson> AddLesson(Lesson g);
        Task<Lesson> UpdateLesson(Lesson g);
        Task<Lesson> DeleteLesson(int id);
        Task<IEnumerable<Lesson>> GetAllLessons();
        Task<Lesson> GetLessonById(int id);
        Task<IEnumerable<Lesson>> GetByIdWithContent(int id);
        Task<IEnumerable<Lesson>> GetByIdWithQuestions(int id);
        Task<IEnumerable<Lesson>> GetByIdWithNotes(int id);
    }
}
