using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IAssignmentGradeService
    {
        Task<AssignmentGrade> AddAssignmentGrade(AssignmentGrade g);
        Task<AssignmentGrade> UpdateAssignmentGrade(AssignmentGrade g);
        Task<AssignmentGrade> DeleteAssignmentGrade(int id);
        Task<IEnumerable<AssignmentGrade>> GetAllAssignmentGrades();
        Task<AssignmentGrade> GetAssignmentGradeById(int id);
    }
}
