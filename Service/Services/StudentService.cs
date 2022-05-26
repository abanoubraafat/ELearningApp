using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class StudentService : IStudentService
    {
        private IStudentRepository iStudentRepository { get; }

        public StudentService(IStudentRepository _iStudentRepository)
        {
            iStudentRepository = _iStudentRepository;
        }
        public async Task<Student> AddStudent(Student s)
        {
            return await iStudentRepository.Add(s);
        }

        public async Task<Student> DeleteStudent(int id)
        {
            return await iStudentRepository.Delete(id);
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await iStudentRepository.GetAll().ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllWithCourses()
        {
            return await iStudentRepository.GetAllWithCourses()
                .Select(s => new Student
                {
                    Id = s.Id,
                    EmailAddress = s.EmailAddress,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Phone = s.Phone,
                    Courses = s.Courses
                })
                .ToListAsync();
        }

        //public async Task<IEnumerable<Student>> GetAllWithCoursesWithGrades()
        //{
        //    return await iStudentRepository.GetAllWithCoursesWithGrades()
        //        .Select(s => new Student
        //        {
        //            Id = s.Id,
        //            emailAddress = s.emailAddress,
        //            StudentFirstName = s.StudentFirstName,
        //            StudentLastName = s.StudentLastName,
        //            StudentPhone = s.StudentPhone,
        //            Courses = s.Courses,
        //            Grades = s.Grades
        //        })
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Student>> GetBYIdWithCourses(int id)
        {
            return await iStudentRepository.GetBYIdWithCourses(id)
                .Select(s => new Student
                {
                    Id = s.Id,
                    EmailAddress = s.EmailAddress,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Phone = s.Phone,
                    Courses = s.Courses
                })
                .ToListAsync();
        }

        //public async Task<IEnumerable<Student>> GetBYIdWithCoursesWithGrades(int id)
        //{
        //    return await iStudentRepository.GetBYIdWithCoursesWithGrades(id)
        //        .Select(s => new Student
        //        {
        //            Id = s.Id,
        //            emailAddress = s.emailAddress,
        //            StudentFirstName = s.StudentFirstName,
        //            StudentLastName = s.StudentLastName,
        //            StudentPhone = s.StudentPhone,
        //            Courses = s.Courses,
        //            Grades = s.Grades
        //        })
        //        .ToListAsync();
        //}

        public async Task<Student> GetStudentById(int id)
        {
            return await iStudentRepository.GetById(id);
        }

        public async Task<Student> UpdateStudent(Student s)
        {
            return await iStudentRepository.Update(s);
        }

        public bool JoinCourseByCourseId(int studentId, int courseId)
        {
          
              if(iStudentRepository.JoinCourseByCourseId(studentId, courseId))
                return true;
              else
                return false;
            
        }

        public async Task<IEnumerable<Student>> GetByIdWithNotes(int id)
        {
            return await iStudentRepository.GetByIdWithNotes(id).ToListAsync();
        }
        //public IQueryable<Student> JoinCourseByCourseId(int studentId, int courseId)
        //{
        //    Student student = GetById(studentId).Result;
        //    Course course = unitOfWork.Context.Courses.FirstOrDefault(c => c.CourseId == courseId);
        //    student.Courses.Add(course);
        //    return Update(student);
        //}
    }
}
