using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface ICourseRepository :IGenericRepository<Course>
    {
        //IQueryable<Course> GetAllWithTeachers();
        //IQueryable<Course> GetAllWithStudents();

        //// specific Course
        //IQueryable<Course> GetByIdWithTeachers(int id);
        Task<Course> GetByIdWithStudents(int id);
        //IQueryable<Course> GetCoursesByTeacherId(int id);
        //IQueryable<Course> GetNotGradedAnswersByCourseId(int id);
        Task<IEnumerable<Course>> GetCoursesByStudentId(int id);
        //IQueryable<Course> GetByIdWithAnnouncements(int id);
        //IQueryable<Course> GetByIdWithAssignments(int id);
        //IQueryable<Course> GetByIdWithQuizes(int id);
        //IQueryable<Course> GetByIdWithLessons(int id);
        //IQueryable<Course> GetByIdWithLatestPassedLesson(int id);
        //IQueryable<Course> GetCoursesByAssId(int id1, int id2);
        ////IQueryable<Course> GetByIdWithAnnouncementId(int id1, int id2);
        ///
        Task<IEnumerable<Course>> GetCoursesByTeacherId(int teacherId);
        Task<string> JoinCourseForStudent(int studentId, int courseId);
        Task<bool> IsValidCourseId(int id);
        Task<string> DropCourseForStudent(int studentId, int courseId);

    }
}
