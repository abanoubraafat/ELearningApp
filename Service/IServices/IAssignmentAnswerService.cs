using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IAssignmentAnswerService
    {
        Task<AssignmentAnswer> AddAssignmentAnswer(AssignmentAnswer g);
        Task<AssignmentAnswer> UpdateAssignmentAnswer(AssignmentAnswer g);
        Task<AssignmentAnswer> DeleteAssignmentAnswer(int id);
        Task<IEnumerable<AssignmentAnswer>> GetAllAssignmentAnswers();
        Task<AssignmentAnswer> GetAssignmentAnswerById(int id);
        Task<IEnumerable<AssignmentAnswer>> GetNotGradedAnswers();
        Task<AssignmentAnswer> GetByIdWithGrade(int id);
        Task<AssignmentAnswer> GetByIdWithFeedback(int id);
        Task<AssignmentAnswer> GetByIdWithBadge(int id);
        Task<bool> isValidAssignmentAnswerFk(int id);

    }
}
