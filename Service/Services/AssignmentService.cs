using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class AssignmentService : IAssignmentService
    {
        private IAssignmentRepository iAssignmentRepository { get; }
        public AssignmentService(IAssignmentRepository _iAssignmentRepository)
        {
            iAssignmentRepository = _iAssignmentRepository;
        }
        public async Task<Assignment> AddAssignment(Assignment a)
        {
            return await iAssignmentRepository.Add(a);
        }

        public async Task<Assignment> DeleteAssignment(int id)
        {
            return await iAssignmentRepository.Delete(id);
        }

        public async Task<IEnumerable<Assignment>> GetAllAssignments()
        {
            return await iAssignmentRepository.GetAll().ToListAsync();
        }

        public async Task<Assignment> GetAssignmentById(int id)
        {
            return await iAssignmentRepository.GetById(id);
        }

        public async Task<Assignment> UpdateAssignment(Assignment a)
        {
            return await iAssignmentRepository.Update(a);
        }

        public async Task<IEnumerable<Assignment>> GetByIdWithCourses(int id1, int id2)
        {
            return await iAssignmentRepository.GetByIdWithCourses(id1, id2).ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAllWithAssignmentAnswers()
        {
            return await iAssignmentRepository.GetAllWithAssignmentAnswers()
                .Select(a => new Assignment
                {
                    Id = a.Id,
                    Title = a.Title,
                    AssignmentAnswers = a.AssignmentAnswers
                }).ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetByIdWithAssignmentAnswers(int id)
        {
            return await iAssignmentRepository.GetByIdWithAssignmentAnswers(id)
                .Select(a => new Assignment
                {
                    Id = a.Id,
                    Title = a.Title,
                    AssignmentAnswers = a.AssignmentAnswers
                }).ToListAsync();
        }
    }
}
