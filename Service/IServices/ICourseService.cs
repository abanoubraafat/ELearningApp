using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface ICourseService
    {
        Task<Course> AddCourse(Course c);
        Task<Course> UpdateCourse(Course c);
        Task<Course> DeleteCourse(int id);
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetCourseById(int id);
        Task<IEnumerable<Course>> GetAllWithTeachers();
        Task<IEnumerable<Course>> GetAllWithStudents();
        Task<IEnumerable<Course>> GetByIdWithTeachers(int id);
        Task<IEnumerable<Course>> GetByIdWithStudents(int id);
        Task<IEnumerable<Course>> GetCoursesByTeacherId(int id);
        //Task<IEnumerable<Course>> GetNotGradedAnswersByCourseId(int id);
        Task<IEnumerable<Course>> GetByIdWithAnnouncements(int id);
        Task<IEnumerable<Course>> GetByIdWithAssignments(int id);
        Task<IEnumerable<Course>> GetByIdWithQuizes(int id);
        Task<IEnumerable<Course>> GetByIdWithLessons(int id);
        Task<IEnumerable<Course>> GetByIdWithLatestPassedLesson(int id);
        Task<IEnumerable<Course>> GetCoursesByAssId(int id1, int id2);

    }
}
