using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IStudentService
    {
        Task<Student> AddStudent(Student s);
        Task<Student> UpdateStudent(Student s);
        Task<Student> DeleteStudent(int id);
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudentById(int id);
        Task<IEnumerable<Student>> GetAllWithCourses();
        //Task<IEnumerable<Student>> GetAllWithCoursesWithGrades();
        Task<IEnumerable<Student>> GetBYIdWithCourses(int id);
        //Task<IEnumerable<Student>> GetBYIdWithCoursesWithGrades(int id);
        bool JoinCourseByCourseId(int studentId, int courseId);
        Task <IEnumerable<Student>> GetByIdWithNotes(int id);
    }
}
