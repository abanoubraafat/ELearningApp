using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class AssignmentGradeService : IAssignmentGradeService
    {
        private IAssignmentGradeRepository iAssignmentGradeRepository { get; }

        public AssignmentGradeService(IAssignmentGradeRepository _iAssignmentGradeRepository)
        {
            iAssignmentGradeRepository = _iAssignmentGradeRepository;
        }
        public async Task<AssignmentGrade> AddAssignmentGrade(AssignmentGrade g)
        {
            return await iAssignmentGradeRepository.Add(g);
        }

        public async Task<AssignmentGrade> DeleteAssignmentGrade(int id)
        {
            return await iAssignmentGradeRepository.Delete(id);
        }

        public async Task<IEnumerable<AssignmentGrade>> GetAllAssignmentGrades()
        {
            return await iAssignmentGradeRepository.GetAll().ToListAsync();
        }

        public async Task<AssignmentGrade> GetAssignmentGradeById(int id)
        {
            return await iAssignmentGradeRepository.GetById(id);
        }

        public async Task<AssignmentGrade> UpdateAssignmentGrade(AssignmentGrade g)
        {
            return await iAssignmentGradeRepository.Update(g);
        }
    }
}
