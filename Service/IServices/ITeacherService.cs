using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface ITeacherService
    {
        Task<Teacher> AddTeacher(Teacher t);
        Task<Teacher> UpdateTeacher(Teacher t);
        Task<Teacher> DeleteTeacher(int id);
        Task<IEnumerable<Teacher>> GetAllTeachers();
        Task<Teacher> GetTeacherById(int id);
        Task<IEnumerable<Teacher>> GetAllWithCourses();
        Task<IEnumerable<Teacher>> GetByIdWithCourses(int id);
    }
}
