#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELearning_App.Domain.Entities;
using Microsoft.Extensions.Logging;
using ELearning_App.Helpers;
using Serilog;
using ELearning_App.Repository.UnitOfWork;

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private ICourseRepository service { get; }
        private readonly IMapper mapper;
        private readonly ITeacherRepository teacherRepository;
        IStudentRepository studentRepository;
        IUnitOfWork unitOfWork;
        public CoursesController(ICourseRepository _service, IMapper mapper, ITeacherRepository teacherRepository, IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCourses , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            try
            {
                if (service.GetByIdAsync(id) == null)
                    return NotFound();
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCourse , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            try
            {
                var isValidTeacherId = await teacherRepository.IsValidTeacherId(course.TeacherId);
                if (!isValidTeacherId)
                    return BadRequest("No Teacher with that id");
                var c = await service.GetByIdAsync(id);
                if (c == null) return NotFound();
                return Ok(await service.Update(course));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: PutCourse , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseDTO dto)
        {
            try
            {
                var isValidTeacherId = await teacherRepository.IsValidTeacherId(dto.TeacherId);
                if(!isValidTeacherId)
                    return BadRequest("No Teacher with that id");
                var course = mapper.Map<Course>(dto);
                return Ok(await service.AddAsync(course));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: PostCourse , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {

                var course = await service.GetByIdAsync(id);
                if (course == null) return NotFound();
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: DeleteCourse , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("Students/{studentId}/JoinCourse/{courseId}")]
        public async Task<ActionResult<Course>> JoinCourseForStudent(int studentId, int courseId)
        {
            try
            {
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                var isValidCourseId = await service.IsValidCourseId(courseId);
                if (!isValidStudentId)
                    return NotFound("Invalid studentId");
                else if (!isValidCourseId)
                    return NotFound("Invalid courseId");

                var added = await service.JoinCourseForStudent(studentId, courseId);
                if (added.Equals("Course Joined Succefully"))
                    return Ok(added);
                else if (added.Equals("Already Joined"))
                    return BadRequest(added);
                return BadRequest(added);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCourse , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("Students/{studentId}/DropCourse/{courseId}")]
        public async Task<ActionResult<Course>> DropCourseForStudent(int studentId, int courseId)
        {
            try
            {
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                var isValidCourseId = await service.IsValidCourseId(courseId);
                if (!isValidStudentId)
                    return NotFound("Invalid studentId");
                else if (!isValidCourseId)
                    return NotFound("Invalid courseId");

                var dropped = await service.DropCourseForStudent(studentId, courseId);
                if (dropped.Equals("Dropped"))
                    return Ok("Course Dropped Succefully");
                else
                    return BadRequest("Invalid studentId or courseId");
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCourse , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        //[HttpGet("GetAllWithTeachers")]
        //public async Task<ActionResult<IEnumerable<Course>>> GetAllWithTeachers()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllWithTeachers());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetAllWithTeachers , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        //[HttpGet("GetAllWithStudents")]
        //public async Task<ActionResult<IEnumerable<Course>>> GetAllWithStudents()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllWithStudents());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetAllWithStudents , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("Courses/{id}/Teachers")]
        //public async Task<ActionResult> GetByIdWithTeachers([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithTeachers(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetByIdWithTeachers , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        [HttpGet("GetByIdWithStudents/{id}")]
        public async Task<ActionResult> GetByIdWithStudents([FromRoute] int id)
        {
            try
            {
                return Ok(await service.GetByIdWithStudents(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetByIdWithStudents , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        // api/GetCoursesByTeacherId/5
        [HttpGet("GetCoursesByTeacherId/{teacherId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByTeacherId(int teacherId)
        {
            try
            {
                var isValidTeacherId = await teacherRepository.IsValidTeacherId(teacherId);
                if (!isValidTeacherId)
                    return BadRequest($"No Teacher with that id: {teacherId}");
                var courses = await service.GetCoursesByTeacherId(teacherId);
                if (courses.Count() == 0) return NotFound("No Courses with that teacherId");
                return Ok(courses);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCoursesByTeacherId , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("GetCoursesByStudentId/{studentId}")]
        public async Task<ActionResult<IEnumerable<CourseDetailsDTO>>> GetCoursesByStudentId(int studentId)
        {
            try
            {
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                if (!isValidStudentId)
                    return BadRequest($"No Student with that id: {studentId}");
                var courses = await service.GetCoursesByStudentId(studentId);
                if (courses.Count() == 0) return NotFound("No Courses with that studentId");
                var mapped = mapper.Map<IEnumerable<CourseDetailsDTO>>(courses);
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCoursesByStudentId , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        //[HttpGet("Students/{studentId}/JoinCourse/{courseId}")]
        //public async Task<ActionResult<Course>> JoinCourse(int studentId, int courseId)
        //{
        //    try
        //    {
        //      return(await service.JoinCourse(studentId, courseId));

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetCourse , Message: {ex.Message}");
        //        return NotFound();
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        // api/GetCoursesByTeacherId/5
        //[HttpGet("GetNotGradedAnswers/{id}")]
        //public async Task<ActionResult<IEnumerable<Course>>> GetNotGradedAnswers([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetNotGradedAnswersByCourseId(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetCoursesByTeacherId , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("Courses/{id}/Announcements")]
        //public async Task<ActionResult> GetByIdWithAnnouncements([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithAnnouncements(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetByIdWithAnnouncements , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("Courses/{id}/Assignments")]
        //public async Task<ActionResult> GetByIdWithAssignments([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithAssignments(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetByIdWithAssignments , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("Courses/{id}/Quizes")]
        //public async Task<ActionResult> GetByIdWithQuizes([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithQuizes(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetByIdWithQuizes , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("Courses/{id}/Lessons")]
        //public async Task<ActionResult> GetByIdWithLessons([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithLessons(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetByIdWithLessons , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        //[HttpGet("Courses/{id}/LatestPassedLesson")]
        //public async Task<ActionResult> GetByIdWithLatestPassedLesson([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithLatestPassedLesson(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetByIdWithLatestPassedLesson , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("Courses/{id1}/Ass/{id2}")]
        //public async Task<ActionResult> GetCoursesByAssId(int id1, int id2)
        //{
        //    try
        //    {
        //        return Ok(await service.GetCoursesByAssId(id1, id2));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetByIdWithLatestPassedLesson , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
    }
}
