using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class AssignmentGradeRepository : GenericRepository<AssignmentGrade>, IAssignmentGradeRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public AssignmentGradeRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<AssignmentGrade> GetAssignmentGradeByAssignmentAnswerId(int assignmentAnswerId)
        {
            return await unitOfWork.Context.AssignmentGrades
                .FirstOrDefaultAsync(a => a.AssignmentAnswerId == assignmentAnswerId);
        }
        public async Task<bool> IsValidAssignmentGradeId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }
    }
}
