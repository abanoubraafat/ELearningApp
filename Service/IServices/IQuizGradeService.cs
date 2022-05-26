using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IQuizGradeService
    {
        Task<QuizGrade> AddQuizGrade(QuizGrade g);
        Task<QuizGrade> UpdateQuizGrade(QuizGrade g);
        Task<QuizGrade> DeleteQuizGrade(int id);
        Task<IEnumerable<QuizGrade>> GetAllQuizGrades();
        Task<QuizGrade> GetQuizGradeById(int id);
    }
}
