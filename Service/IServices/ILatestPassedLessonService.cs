using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface ILatestPassedLessonService
    {
        Task<LatestPassedLesson> AddLatestPassedLesson(LatestPassedLesson g);
        Task<LatestPassedLesson> UpdateLatestPassedLesson(LatestPassedLesson g);
        Task<LatestPassedLesson> DeleteLatestPassedLesson(int id);
        Task<IEnumerable<LatestPassedLesson>> GetAllLatestPassedLessons();
        Task<LatestPassedLesson> GetLatestPassedLessonById(int id);
    }
}
