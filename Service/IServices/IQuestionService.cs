using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IQuestionService
    {
        Task<Question> AddQuestion(Question g);
        Task<Question> UpdateQuestion(Question g);
        Task<Question> DeleteQuestion(int id);
        Task<IEnumerable<Question>> GetAllQuestions();
        Task<Question> GetQuestionById(int id);
        Task<Question> GetByIdWithAnswers(int id);

    }
}
