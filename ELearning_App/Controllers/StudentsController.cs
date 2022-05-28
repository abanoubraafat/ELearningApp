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
        public StudentsController(IStudentRepository _service, IMapper mapper)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
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
                return Ok(await service.GetStudentByEmail(email));
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
