using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class CourseService : ICourseService
    {
        private ICourseRepository iCourseRepository { get; }

        public CourseService(ICourseRepository _iCourseRepository)
        {
            iCourseRepository = _iCourseRepository;
        }

        public async Task<Course> AddCourse(Course c)
        {
            return await iCourseRepository.Add(c);
        }

        public async Task<Course> DeleteCourse(int id)
        {
            return await iCourseRepository.Delete(id);
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await iCourseRepository.GetAll()
                //.Select(c => new Course
                //{
                //    Id = c.Id,
                //    CourseName = c.CourseName,
                //    CourseDescription = c.CourseDescription,
                //    Students = c.Students,
                //    TeacherId = c.TeacherId,
                //    Teacher = c.Teacher,
                //    Grades = c.Grades,
                //    Videos = c.Videos,
                //    PDFFiles = c.PDFFiles
                //})
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllWithStudents()
        {
            return await iCourseRepository.GetAllWithStudents()
                .Select(c => new Course
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    Students = c.Students,
                    //Grades = c.Grades
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllWithTeachers()
        {
            return await iCourseRepository.GetAllWithTeachers()
                .Select(c => new Course
                {
                    Id= c.Id,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    TeacherId = c.TeacherId,
                    Teacher = c.Teacher
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByIdWithStudents(int id)
        {
            return await iCourseRepository.GetByIdWithStudents(id)
                .Select(c => new Course
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    Students = c.Students
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByIdWithTeachers(int id)
        {
            return await iCourseRepository.GetByIdWithTeachers(id)
                .Select(c => new Course
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    TeacherId = c.TeacherId,
                    Teacher = c.Teacher,
                })
                .ToListAsync();
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await iCourseRepository.GetById(id);
            //return await iCourseRepository.GetAll()
            //     .Select(c => new Course
            //     {
            //         Id = c.Id,
            //         CourseName = c.CourseName,
            //         CourseDescription = c.CourseDescription,
            //         Students = c.Students,
            //         TeacherId = c.TeacherId,
            //         Teacher = c.Teacher,
            //         Grades = c.Grades,
            //         Videos = c.Videos,
            //         PDFFiles = c.PDFFiles
            //     }).FirstOrDefaultAsync(c => c.Id == id );
        }

        public async Task<Course> UpdateCourse(Course c)
        {
            return await iCourseRepository.Update(c);
        }
        public async Task<IEnumerable<Course>> GetCoursesByTeacherId(int id)
        {
            return await iCourseRepository.GetCoursesByTeacherId(id).ToListAsync();
        }

        //public async Task<IEnumerable<Course>> GetNotGradedAnswersById(int id)
        //{
        //    return await iCourseRepository.GetNotGradedAnswersById(id)
        //                                                                    //.Select(c => new Course
        //                                                                    //{
        //                                                                    //    Id= c.Id,
        //                                                                    //    CourseName = c.CourseName,
        //                                                                    //    CourseImage = c.CourseImage,

        //                                                                    //})
        //                                                                    .ToListAsync();
        //}

        public async Task<IEnumerable<Course>> GetByIdWithAnnouncements(int id)
        {
            return await iCourseRepository.GetByIdWithAnnouncements(id).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByIdWithAssignments(int id)
        {
            return await iCourseRepository.GetByIdWithAssignments(id).Select(
                c => new Course
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    CourseImage = c.CourseImage,
                    Assignments = c.Assignments
                }
                ).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByIdWithQuizes(int id)
        {
            return await iCourseRepository.GetByIdWithQuizes(id).Select(
                c => new Course
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    CourseImage = c.CourseImage,
                    Quizzes = c.Quizzes
                }
                ).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByIdWithLessons(int id)
        {
            return await iCourseRepository.GetByIdWithLessons(id).Select(
                c => new Course
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    CourseImage = c.CourseImage,
                    Lessons = c.Lessons
                }
                ).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByIdWithLatestPassedLesson(int id)
        {
            return await iCourseRepository.GetByIdWithLatestPassedLesson(id).Select(
                c => new Course
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    CourseImage = c.CourseImage,
                    LatestPassedLessons = c.LatestPassedLessons
                }
                ).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByAssId(int id1, int id2)
        {
            return await iCourseRepository.GetCoursesByAssId(id1, id2).Select(c => new Course
            {
                Assignments = c.Assignments
            }).ToListAsync();
        }
    }
}
