using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public StudentRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<bool> IsValidStudentId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        public async Task<bool> IsValidStudentEmail(string email)
        {
            return await IsValidFk(s => s.EmailAddress == email);
        }

        public async Task<Student> GetStudentByEmail(string email)
        {
            return await unitOfWork.Context.Students.FirstAsync(s => s.EmailAddress == email);
        }

        //Implementations

        //public IQueryable<Student> GetAllWithCourses()
        //{
        //    return unitOfWork.Context.Students.Include(s => s.Courses);
        //}

        ////public IQueryable<Student> GetAllWithCoursesWithGrades()
        ////{
        ////    return unitOfWork.Context.Students.Include(s => s.Courses).Include(s => s.Grades);
        ////}

        //public IQueryable<Student> GetBYIdWithCourses(int id)
        //{
        //    return unitOfWork.Context.Students.Where(s => s.Id == id).Include(s => s.Courses);
        //}

        //public bool JoinCourseByCourseId(int studentId, int courseId)
        //{

        //    Student student = GetById(studentId).Result;
        //    Course course = unitOfWork.Context.Courses.FirstOrDefault(c => c.Id == courseId);
        //    if (course == null || student == null)
        //    { return false; }
        //    else
        //    {
        //        student.Courses.Add(course);
        //        Update(student);
        //        return true;
        //    }
        //}

        //public IQueryable<Student> GetByIdWithNotes(int id)
        //{
        //    return unitOfWork.Context.Students.Where(s=> s.Id == id).Include(s => s.Notes);
        //}

        ////public IQueryable<Student> GetBYIdWithCoursesWithGrades(int id)
        ////{
        ////    return unitOfWork.Context.Students.Where(s => s.Id == id).Include(s => s.Courses).Include(s => s.Grades);
        ////}
    }
}
