using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IParentService
    {
        Task<Parent> AddParent(Parent p);
        Task<Parent> UpdateParent(Parent p);
        Task<Parent> DeleteParent(int id);
        Task<IEnumerable<Parent>> GetAllParents();
        Task<Parent> GetParentById(int id);
        Task<IEnumerable<Parent>> GetAllWithStudentWithCoursesWithGrades();
        Task<IEnumerable<Parent>> GetByIdWithStudentWithCoursesWithGrades(int id);
    }
}
