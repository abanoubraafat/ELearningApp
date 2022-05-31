using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        //IQueryable<Student> GetAllWithCourses();
        ////IQueryable<Student> GetAllWithCoursesWithGrades();

        //IQueryable <Student> GetBYIdWithCourses(int id);
        ////IQueryable <Student> GetBYIdWithCoursesWithGrades(int id);
        //bool JoinCourseByCourseId(int studentId, int courseId);
        //IQueryable<Student> GetByIdWithNotes(int id);
        Task<bool> IsValidStudentId(int id);
        Task<bool> IsValidStudentEmail(string email);
        Task<Student> GetStudentByEmail(string email);
        Task<IEnumerable<Student>> GetStudentsByCourseId(int courseId);

    }
}
