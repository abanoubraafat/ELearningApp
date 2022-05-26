using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class AssignmentFeedbackService : IAssignmentFeedbackService
    {
        private readonly IAssignmentFeedbackRepository iAssignmentFeedbackRepository;

        public AssignmentFeedbackService(IAssignmentFeedbackRepository _iAssignmentFeedbackRepository)
        {
            iAssignmentFeedbackRepository = _iAssignmentFeedbackRepository;
        }
        public async Task<AssignmentFeedback> AddAssignmentFeedback(AssignmentFeedback g)
        {
            return await iAssignmentFeedbackRepository.Add(g);
        }

        public async Task<AssignmentFeedback> DeleteAssignmentFeedback(int id)
        {
            return await iAssignmentFeedbackRepository.Delete(id);
        }

        public async Task<IEnumerable<AssignmentFeedback>> GetAllAssignmentFeedbacks()
        {
            return await iAssignmentFeedbackRepository.GetAll().ToListAsync();
        }

        public async Task<AssignmentFeedback> GetAssignmentFeedbackById(int id)
        {
            return await iAssignmentFeedbackRepository.GetById(id);
        }

        public async Task<AssignmentFeedback> UpdateAssignmentFeedback(AssignmentFeedback g)
        {
            return await iAssignmentFeedbackRepository.Update(g);
        }
    }
}
