using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IAssignmentService
    {
        Task<Assignment> AddAssignment(Assignment g);
        Task<Assignment> UpdateAssignment(Assignment g);
        Task<Assignment> DeleteAssignment(int id);
        Task<IEnumerable<Assignment>> GetAllAssignments();
        Task<Assignment> GetAssignmentById(int id);
        Task<IEnumerable<Assignment>> GetByIdWithCourses(int id1, int id2);
        public Task<IEnumerable<Assignment>> GetAllWithAssignmentAnswers();
        public Task<IEnumerable<Assignment>> GetByIdWithAssignmentAnswers(int id);


    }
}
