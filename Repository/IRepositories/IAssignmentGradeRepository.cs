using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IAssignmentGradeRepository : IGenericRepository<AssignmentGrade>
    {
        Task<AssignmentGrade> GetAssignmentGradeByAssignmentAnswerId(int assignmentAnswerId);
        Task<bool> IsValidAssignmentGradeId(int id);
    }
}
