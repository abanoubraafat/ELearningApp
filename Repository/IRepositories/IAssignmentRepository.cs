using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IAssignmentRepository : IGenericRepository<Assignment>
    {
        //IQueryable<Assignment> GetByIdWithCourses(int id1, int id2);
        //IQueryable<Assignment> GetAllWithAssignmentAnswers();
        //IQueryable<Assignment> GetByIdWithAssignmentAnswers(int id);
        //IEnumerable<Assignment> GetAssignmentsByCourseId(int courseId);
        Task<IEnumerable<Assignment>> GetAssignmentsByCourseId(int courseId);
        Task<IEnumerable<Assignment>> GetAssignmentsByCourseIdForStudent(int courseId);
        Task<bool> IsValidAssignmentId(int id);

    }
}
