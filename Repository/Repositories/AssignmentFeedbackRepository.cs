using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class AssignmentFeedbackRepository : GenericRepository<AssignmentFeedback>, IAssignmentFeedbackRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public AssignmentFeedbackRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<AssignmentFeedback> GetAssignmentFeedbackByAssignmentAnswerId(int assignmentAnswerId)
        {
            return await unitOfWork.Context.AssignmentFeedbacks
                .FirstAsync(a => a.AssignmentAnswerId == assignmentAnswerId);
        }

        public async Task<bool> IsValidAssignmentFeedbackId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }
    }
}
