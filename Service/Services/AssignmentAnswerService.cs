using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class AssignmentAnswerService : IAssignmentAnswerService
    {
        private IAssignmentAnswerRepository iAssignmentAnswerRepository { get; }

        public AssignmentAnswerService(IAssignmentAnswerRepository _iAssignmentAnswerRepository)
        {
            iAssignmentAnswerRepository = _iAssignmentAnswerRepository;
        }
        public async Task<AssignmentAnswer> AddAssignmentAnswer(AssignmentAnswer a)
        {
            return await iAssignmentAnswerRepository.Add(a);
        }

        public async Task<AssignmentAnswer> DeleteAssignmentAnswer(int id)
        {
            return await iAssignmentAnswerRepository.Delete(id); 
        }

        public async Task<IEnumerable<AssignmentAnswer>> GetAllAssignmentAnswers()
        {
            return await iAssignmentAnswerRepository.GetAll().ToListAsync();
        }

        public async Task<AssignmentAnswer> GetAssignmentAnswerById(int id)
        {
            return await iAssignmentAnswerRepository.GetById(id);
        }

        public async Task<AssignmentAnswer> UpdateAssignmentAnswer(AssignmentAnswer a)
        {
            return await iAssignmentAnswerRepository.Update(a);
        }

        public async Task<IEnumerable<AssignmentAnswer>> GetNotGradedAnswers()
        {
            return await iAssignmentAnswerRepository.GetNotGradedAnswers().ToListAsync();
        }

        public async Task<AssignmentAnswer> GetByIdWithGrade(int id)
        {
            return await iAssignmentAnswerRepository.GetByIdWithGrade(id)
                .Select(a => new AssignmentAnswer
                {
                    Id = a.Id,
                    FileName = a.FileName,
                    AssignmentGrade = a.AssignmentGrade
                })
                .FirstAsync();
        }

        public async Task<AssignmentAnswer> GetByIdWithFeedback(int id)
        {
            return await iAssignmentAnswerRepository.GetByIdWithFeedback(id)
                .Select(a => new AssignmentAnswer
                {
                    Id = a.Id,
                    FileName = a.FileName,
                    AssignmentFeedback = a.AssignmentFeedback
                })
                .FirstAsync();
        }

        public async Task<AssignmentAnswer> GetByIdWithBadge(int id)
        {
            return await iAssignmentAnswerRepository.GetByIdWithBadge(id)
                .Select(a => new AssignmentAnswer
                {
                    Id = a.Id,
                    FileName = a.FileName,
                    Badge = a.Badge
                })
                .FirstAsync();
        }

        public async Task<bool> isValidAssignmentAnswerFk(int id)
        {
            return await iAssignmentAnswerRepository.isValidAssignmentAnswerFk(id);
        }
    }
}
