using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IQuestionAnswerService
    {
        Task<QuestionAnswer> AddQuestionAnswer(QuestionAnswer g);
        Task<QuestionAnswer> UpdateQuestionAnswer(QuestionAnswer g);
        Task<QuestionAnswer> DeleteQuestionAnswer(int id);
        Task<IEnumerable<QuestionAnswer>> GetAllQuestionAnswers();
        Task<QuestionAnswer> GetQuestionAnswerById(int id);
    }
}
