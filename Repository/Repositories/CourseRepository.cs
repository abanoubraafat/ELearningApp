using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        //private IQueryable<Course> u1;
        //private IQueryable<Announcement> u2;

        private IUnitOfWork unitOfWork { get;  }
        public CourseRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Course>> GetCoursesByTeacherId(int teacherId)
        {
            return await unitOfWork.Context.Courses
                .Where(c => c.TeacherId == teacherId)
                .ToListAsync();
        }
        public async Task<bool> IsValidCourseId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        public async Task<Course> JoinCourse(int studentId, int courseId)
        {
            //    Student student = GetById(studentId).Result;
            //    Course course = unitOfWork.Context.Courses.FirstOrDefault(c => c.Id == courseId);
            //    if (course == null || student == null)
            //    { return false; }
            //    else
            //    {
            //        student.Courses.Add(course);
            //        Update(student);
            Student s = await unitOfWork.Context.Students.FirstAsync(s => s.Id == studentId);
            Course c = await GetByIdAsync(courseId);
            c.Students.Add(s);
            await Update(c);
            return c;
        }
        //not complete
        public async Task<IEnumerable<Course>> GetCoursesByStudentId(int id)
        {
            return await unitOfWork.Context.Courses.Include(c => c.Students).Include(c => c.Teacher)
                .Where(s => s.Students.Any(s => s.Id == id)).Select(c => new Course
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                }).ToListAsync();
        }

        //public IQueryable<Course> GetAllWithStudents()
        //{
        //    return unitOfWork.Context.Courses.Include(c => c.Students);
        //}

        //public IQueryable<Course> GetAllWithTeachers()
        //{
        //    return unitOfWork.Context.Courses.Include(c => c.Teacher);
        //}

        //public IQueryable<Course> GetByIdWithStudents(int id)
        //{
        //    return unitOfWork.Context.Courses.Where(c => c.Id == id).Include(c => c.Students);
        //}

        //public IQueryable<Course> GetByIdWithTeachers(int id)
        //{
        //    return unitOfWork.Context.Courses.Where(c => c.Id == id).Include(c => c.Teacher);
        //}

        //public IQueryable<Course> GetCoursesByTeacherId(int id)
        //{
        //    return unitOfWork.Context.Courses.Where(c => c.TeacherId == id);
        //}

        //public IQueryable<Course> GetNotGradedAnswersById(int id)
        //{
        //    return unitOfWork.Context.Courses.Include(c => c.Assignments).ThenInclude(a => a.AssignmentAnswers).ThenInclude(aa => aa.AssignmentGrade)
        //                                      .Where((c => (c.Id == id) && (c.Assignments.Select(a => a.AssignmentAnswers
        //                                                              .Select(ans => ans.AssignmentGrade))
        //                                                                == null)));
        //                                      //.Include(c => c.Assignments.Select(a => a.AssignmentAnswers));
        //}

        //public IQueryable<Course> GetByIdWithAnnouncements(int id)
        //{
        //    return unitOfWork.Context.Courses.Where(c => c.Id == id).Include(c => c.Announcements);
        //}


        //public IQueryable<Course> GetByIdWithQuizes(int id)
        //{
        //    return unitOfWork.Context.Courses.Where(c => c.Id == id).Include(c => c.Quizzes);
        //}

        //public IQueryable<Course> GetByIdWithLessons(int id)
        //{
        //    return unitOfWork.Context.Courses.Where(c => c.Id == id).Include(c => c.Lessons);
        //}

        //public IQueryable<Course> GetByIdWithLatestPassedLesson(int id)
        //{
        //    return unitOfWork.Context.Courses.Where(c => c.Id == id).Include(c => c.LatestPassedLessons);
        //}

        ////public IQueryable<Course> GetByIdWithAnnouncementId(int id1, int id2)
        ////{
        ////    u1 = unitOfWork.Context.Courses.Where(c => c.Id == id1);
        ////    u2 = unitOfWork.Context.Announcements.Where(a => a.AnnouncementId == id2);
        ////    return unitOfWork.Context.C
        ////}


        //public IQueryable<Course> GetByIdWithAssignments(int id)
        //{
        //    return unitOfWork.Context.Courses.Where(c => c.Id == id).Include(c => c.Assignments);
        //}

        //public IQueryable<Course> GetCoursesByAssId(int id1, int id2)
        //{
        //    return GetByIdWithAssignments(id1).Where(c => c.Assignments.Select(a => a.Id).FirstOrDefault() == id2); ;
        //    //return unitOfWork.Context.Courses.Include(c => c.Assignments).Where(c => c.Id == id1 && c.Assignments.Select(a => a.AssignmentId).FirstOrDefault() == id2);
        //}

        //public IQueryable<Course> GetNotGradedAnswersByCourseId(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
