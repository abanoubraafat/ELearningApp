using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IAssignmentFeedbackService
    {
        Task<AssignmentFeedback> AddAssignmentFeedback(AssignmentFeedback g);
        Task<AssignmentFeedback> UpdateAssignmentFeedback(AssignmentFeedback g);
        Task<AssignmentFeedback> DeleteAssignmentFeedback(int id);
        Task<IEnumerable<AssignmentFeedback>> GetAllAssignmentFeedbacks();
        Task<AssignmentFeedback> GetAssignmentFeedbackById(int id);
    }
}
