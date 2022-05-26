using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IQuizAnswerService
    {
        Task<QuizAnswer> AddQuizAnswer(QuizAnswer g);
        Task<QuizAnswer> UpdateQuizAnswer(QuizAnswer g);
        Task<QuizAnswer> DeleteQuizAnswer(int id);
        Task<IEnumerable<QuizAnswer>> GetAllQuizAnswers();
        Task<QuizAnswer> GetQuizAnswerById(int id);
        Task<QuizAnswer> GetByIdWithGrade(int id);
    }
}
