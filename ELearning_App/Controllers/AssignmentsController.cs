using ELearning_App.Domain.Entities;
using ELearning_App.Helpers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace ELearning_App.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private IAssignmentRepository service { get; }
        private readonly ICourseRepository courseService;
        public AssignmentsController(IAssignmentRepository _service, IMapper mapper, ICourseRepository courseService)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.courseService = courseService;
        }

        // GET: api/Assignmentes
        [HttpGet("Assignments")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments()
        {
            try
            {
                var a = await service.GetAllAsync();
            var mapped = mapper.Map<IEnumerable<AssignmentDTO>>(a);
            return Ok(mapped);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentController , Action: GetAssignments , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/Assignmentes/5
        [HttpGet("Assignments/{id}")]
        public async Task<ActionResult<Assignment>> GetAssignment(int id)
        {
            try
            {
                var a = await service.GetByIdAsync(id);
                if (a == null)
                    return NotFound($"No Assignmetn was found with Id: {id}");
                return Ok(mapper.Map<AssignmentDTO>(a));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentController , Action: GetAssignment , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/Assignmentes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Assignments/{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, AssignmentDTO dto)
        {
            try
            {
                var isValidCourseId = await courseService.IsValidCourseId(dto.CourseId);
                if (!isValidCourseId)
                    return BadRequest("Invalid CourseId!");

                var assignment = await service.GetByIdAsync(id);
                if (assignment == null) return NotFound($"No Assignment was found with Id: {id}");
                //var r = mapper.Map<Resource>(dto);
                assignment.CourseId = dto.CourseId;
                assignment.Title = dto.Title;
                assignment.Description = dto.Description;
                assignment.FilePath = dto.FilePath;
                //assignment.DeadlineDate = dto.DeadlineDate;
                //assignment.DeadlineTime = dto.DeadlineTime;
                assignment.Grade = dto.Grade;
                return Ok(await service.Update(assignment));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentController , Action: UpdateAssignment , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/Assignmentes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Assignments")]
        public async Task<ActionResult<Assignment>> AddAssignment(AssignmentDTO dto)
        {
            try
            {
                var isValidCourseId = await courseService.IsValidCourseId(dto.CourseId);
                if (!isValidCourseId)
                    return BadRequest("Invalid CourseId!");
                var r = mapper.Map<Assignment>(dto);
                return Ok(mapper.Map<AssignmentDTO>(await service.AddAsync(r)));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentController , Action: AddAssignment , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/Assignmentes/5
        [HttpDelete("Assignments/{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            try
            {
                var assignment = await service.GetByIdAsync(id);
                if (assignment == null)
                    return NotFound($"No Assignment was found with Id: {id}");
                //var delete = await service.Delete(id);
                //var x = mapper.Map<AssignmentDTO>(delete);
                var a = await service.Delete(id);
                return Ok(mapper.Map<AssignmentDTO>(a));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentController , Action: DeleteAssignment , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("Course/{courseId}/Assignments")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentsByCourseId(int courseId)
        {
            try
            {
                var isValidCourseId = await courseService.IsValidCourseId(courseId);
                if (!isValidCourseId)
                    return BadRequest("Invalid CourseId!");
                var a = await service.GetAssignmentsByCourseId(courseId);
                if (a.Count() == 0)
                    return NotFound($"No Assignments were found with CourseId: {courseId}");
                return Ok(a);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentController , Action: GetAssignmentsByCourseId , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        //// GET: api/Assignmentes/5
        //[HttpGet("Courses/{id1}/Assignments/{id2}")]
        //public async Task<ActionResult<Assignment>> GetByIdWithCourses(int id1, int id2)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithCourses(id1, id2));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: AssignmentController , Action: GetByIdWithCourses , Message: {ex.Message}");
        //        return NotFound();
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("Assignments/GetAllWithAssignmentAnswers")]
        //public async Task<ActionResult<Assignment>> GetAllWithAssignmentAnswers()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllWithAssignmentAnswers());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: AssignmentController , Action: GetAllWithAssignmentAnswers , Message: {ex.Message}");
        //        return NotFound();
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("Assignments/GetByIdWithAssignmentAnswers/{id}")]
        //public async Task<ActionResult<Assignment>> GetByIdWithAssignmentAnswers(int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithAssignmentAnswers(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: AssignmentController , Action: GetByIdWithAssignmentAnswers , Message: {ex.Message}");
        //        return NotFound();
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

    }
}
