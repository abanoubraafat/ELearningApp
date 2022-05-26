using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IQuizService
    {
        Task<Quiz> AddQuiz(Quiz g);
        Task<Quiz> UpdateQuiz(Quiz g);
        Task<Quiz> DeleteQuiz(int id);
        Task<IEnumerable<Quiz>> GetAllQuizes();
        Task<Quiz> GetQuizById(int id);
        Task<Quiz> GetByIdWithAnswers(int id);
    }
}
