using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using System.Linq.Expressions;

namespace ELearning_App.Repository.IRepositories
{
    public interface IAssignmentFeedbackRepository : IGenericRepository<AssignmentFeedback>
    {
        Task<AssignmentFeedback> GetAssignmentFeedbackByAssignmentAnswerId(int assignmentAnswerId);
        Task<bool> IsValidAssignmentFeedbackId(int id);

    }
}
