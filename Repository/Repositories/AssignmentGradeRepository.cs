//using ELearning_App.Domain.Entities;
//using ELearning_App.Repository.GenericRepositories;
//using ELearning_App.Repository.IRepositories;
//using ELearning_App.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ELearning_App.Repository.Repositories
//{
//    public class AssignmentGradeRepository : GenericRepository<AssignmentGrade>, IAssignmentGradeRepository
//    {
//        private IUnitOfWork unitOfWork { get; }
//        public AssignmentGradeRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
//        {
//            unitOfWork = _unitOfWork;
//        }

//        public async Task<AssignmentGrade> GetAssignmentGradeByAssignmentAnswerId(int assignmentAnswerId)
//        {
//            return await unitOfWork.Context.AssignmentGrades
//                .SingleOrDefaultAsync(a => a.AssignmentAnswerId == assignmentAnswerId);
//        }
//        public async Task<bool> IsValidAssignmentGradeId(int id)
//        {
//            return await IsValidFk(a => a.Id == id);
//        }

//        public async Task<bool> IsNotValidAssignmentGradeWithAssignmentAnswerId(int assignmentAnswerId)
//        {
//            return await unitOfWork.Context.AssignmentGrades.AnyAsync(g => g.AssignmentAnswerId == assignmentAnswerId);
//        }

//        public async Task<int> GetIntAssignmentGrade(int assignmentId, int studentId)
//        {
//            var a = await unitOfWork.Context.AssignmentAnswers.SingleOrDefaultAsync(a => a.AssignmentId == assignmentId && a.StudentId == studentId);
//            if (a == null)
//                return -1;
//            else
//            {
//                var assignmentgrade = await unitOfWork.Context.AssignmentGrades.SingleOrDefaultAsync(grade => grade.AssignmentAnswerId == a.Id);
//                if (assignmentgrade == null)
//                    return -1;
//                return assignmentgrade.Grade;
//            }
//        }
//    }
//}
