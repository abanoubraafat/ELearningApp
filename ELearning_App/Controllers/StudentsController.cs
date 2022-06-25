#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELearning_App.Domain.Entities;
using ELearning_App.Domain.Context;
using Serilog;

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentRepository service { get; }
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IParentRepository parentRepository;
        public StudentsController(IStudentRepository _service, IMapper mapper, IUserRepository userRepository, ICourseRepository courseRepository, IParentRepository parentRepository)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.courseRepository = courseRepository;
            this.parentRepository = parentRepository;
        }

        //// GET: api/Students
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllAsync());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: StudentsController , Action: GetStudents , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        //// GET: api/Students/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Student>> GetStudent(int id)
        //{
        //    try
        //    {
        //        if (service.GetByIdAsync(id) == null)
        //            return NotFound();
        //        return Ok(await service.GetByIdAsync(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: StudentsController , Action: GetStudent , Message: {ex.Message}");
        //        return NotFound();
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStudent(int id, Student student)
        //{
        //    try
        //    {
        //        if (id != student.Id)
        //        {
        //            return BadRequest();
        //        }
        //        return Ok(await service.Update(student));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: StudentsController , Action: PutStudent , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentDTO dto)
        {

            try
            {
                var isNotAvailableUserEmail = await userRepository.IsNotAvailableUserEmail(dto.EmailAddress);
                if (isNotAvailableUserEmail)
                    return BadRequest("There's already an account with the same Email address");
                else if (!dto.Role.Equals("Student"))
                    return BadRequest("Make sure the Role field is 'Student'");
                string hashedPassword = userRepository.CreatePasswordHash(dto.Password);
                dto.Password = hashedPassword;
                var student = mapper.Map<Student>(dto);
                return Ok(await service.AddAsync(student));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: StudentsController , Action: PostStudent , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("/Email/{email}")]
        public async Task<ActionResult<Student>> GetStudentByEmail(string email)
        {
            //try
            //{
            var isValidStudentEmail = await service.IsValidStudentEmail(email);
            if (!isValidStudentEmail) return NotFound("Invalid Email");
            var student = await service.GetStudentByEmail(email);
            if (student == null) return NotFound($"No Student was found with that email: {email}");
            return Ok(student);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error($"Controller: StudentsController , Action: GetStudents , Message: {ex.Message}");
            //    return StatusCode(500);
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
        }
        [HttpGet("GetStudentsByCourseId/{courseId}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByCourseId(int courseId)
        {
            try
            {
                var isValidCourseId = await courseRepository.IsValidCourseId(courseId);
                if (!isValidCourseId)
                    return BadRequest($"No Teacher with that id: {courseId}");
                var students = await service.GetStudentsByCourseId(courseId);
                if (students.Count() == 0) return NotFound("No Students with that courseId");

                return Ok(students);
        }
            catch (Exception ex)
            {
                Log.Error($"Controller: StudentsController , Action: PostStudent , Message: {ex.Message}");
                return StatusCode(500);
    }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpGet("GetStudentsByParentId/{parentId}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByParentId(int parentId)
        {
            try
            {
                var isValidParentId = await parentRepository.IsValidParentId(parentId);
                if (!isValidParentId)
                    return BadRequest($"No Parent with that id: {parentId}");
                var students = await service.GetStudentsByParentId(parentId);
                if (students.Count() == 0) return NotFound($"No Students with that parentId :{parentId}");
                return Ok(students);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: StudentsController , Action: PostStudent , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/Students/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStudent(int id)
        //{
        //    try
        //    {
        //        return Ok(await service.Delete(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: StudentsController , Action: DeleteStudent , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        //[HttpGet("GetAllWithCourses")]
        //public async Task<ActionResult<IEnumerable<Student>>> GetAllWithCourses()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllWithCourses());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: StudentsController , Action: GetAllWithCourses , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        //[HttpGet("GetAllWithCoursesWithGrades")]
        //public async Task<ActionResult<IEnumerable<Student>>> GetAllWithCoursesWithGrades()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllWithCoursesWithGrades());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: StudentsController , Action: GetAllWithCoursesWithGrades , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("GetBYIdWithCourses/{id}")]
        //public async Task<ActionResult<Student>> GetBYIdWithCourses([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetBYIdWithCourses(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: StudentsController , Action: GetBYIdWithCourses , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("GetBYIdWithCoursesWithGrades/{id}")]
        //    public async Task<ActionResult<Student>> GetBYIdWithCoursesWithGrades([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetBYIdWithCoursesWithGrades(id));
        //    }
        //    catch (Exception ex)
        //    {
        //            Log.Error($"Controller: StudentsController , Action: GetBYIdWithCoursesWithGrades , Message: {ex.Message}");
        //            return StatusCode(500);
        //    }
        //        finally
        //        {
        //            Log.CloseAndFlush();
        //        }
        //    }
        //[HttpGet("JoinCourseByCourseId/{studentId}/{courseId}")]
        //public bool JoinCourseByCourseId(int studentId, int courseId)
        //{
        //    try
        //    {
        //        if (service.JoinCourseByCourseId(studentId, courseId))
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: StudentsController , Action: JoinCourseByCourseId , Message: {ex.Message}");
        //        StatusCode(500);
        //        return false;
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //    return false;
        //}
        //[HttpGet("GetByIdWithNotes/{id}")]
        //public async Task<ActionResult<Student>> GetByIdWithNotes([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithNotes(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: StudentsController , Action: GetByIdWithNotes , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
    }
}
