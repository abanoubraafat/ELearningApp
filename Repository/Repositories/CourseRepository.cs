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
                .Select(c => new Course
                { 
                    Id = c.Id,
                    CourseName = c.CourseName,
                    CourseImage = c.CourseImage,
                    TeacherId = c.TeacherId
                })
                .ToListAsync();
        }
        public async Task<bool> IsValidCourseId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        public async Task<string> JoinCourseForStudent(int studentId, int courseId)
        {
            
            var student = await unitOfWork.Context.Students.FirstAsync(s => s.Id == studentId);
            var course = await unitOfWork.Context.Courses.Where(c => c.Id == courseId).Include(c => c.Students).FirstAsync();
            if (student == null || course == null)
                return "Invalid studentId or courseId";
            else if (course.Students.Any(s => s.Id == studentId))
                return "Already Joined";
            course.Students.Add(student);
            await Update(course);
            return "Course Joined Succefully";
        }

        public async Task<string> DropCourseForStudent(int studentId, int courseId)
        {
            var student = await unitOfWork.Context.Students.FirstAsync(s => s.Id == studentId);
            var course = await unitOfWork.Context.Courses.Where(c => c.Id == courseId).Include(c => c.Students).FirstAsync();
            if (student == null || course == null || !course.Students.Any(s => s.Id == studentId))
                return "Invalid studentId or courseId";
            course.Students.Remove(student);
            await Update(course);
            return "Dropped";
        }
        //teacher name
        public async Task<IEnumerable<Course>> GetCoursesByStudentId(int id)
        {
            return await unitOfWork.Context.Courses.Include(c => c.Teacher)
                .Where(c => c.Students.Any(s => s.Id == id))
                //.Select(c => new Course
                //{
                //    Id = c.Id,
                //    CourseName = c.CourseName,
                //    //CourseDescription = c.CourseDescription,
                //    CourseImage = c.CourseImage,
                //    //Students = c.Students,
                //    TeacherId = c.TeacherId,
                //    Teacher = c.Teacher
                //})
                .ToListAsync();
        }

        //public IQueryable<Course> GetAllWithStudents()
        //{
        //    return unitOfWork.Context.Courses.Include(c => c.Students);
        //}

        //public IQueryable<Course> GetAllWithTeachers()
        //{
        //    return unitOfWork.Context.Courses.Include(c => c.Teacher);
        //}

        public async Task<Course> GetByIdWithStudents(int id)
        {
            return await unitOfWork.Context.Courses.Where(c => c.Id == id)
                .Include(c => c.Students)
                .Select(c => new Course
                {
                    Id = c.Id,
                    Students = c.Students,
                    CourseName = c.CourseName
                }).FirstOrDefaultAsync();
            //var course = unitOfWork.Context.Courses.Where(c => c.Id == id).Include(c => c.Students).FirstAsync().Result;
            //if (course.Students.Any(s => s.Id == studentId))
            //    return true;
            //else
            //    return false;
        }



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
