using ELearning_App.Domain.Entities;
using ELearning_App.Helpers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private IAssignmentRepository service { get; }
        private readonly ICourseRepository courseService;
        private readonly IAssignmentAnswerRepository assignmentAnswerRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IAssignmentGradeRepository assignmentGradeRepository;
        public AssignmentsController(IAssignmentRepository _service, IMapper mapper, ICourseRepository courseService, IAssignmentAnswerRepository assignmentAnswerRepository, IStudentRepository studentRepository, IAssignmentGradeRepository assignmentGradeRepository)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.courseService = courseService;
            this.assignmentAnswerRepository = assignmentAnswerRepository;
            this.studentRepository = studentRepository;
            this.assignmentGradeRepository = assignmentGradeRepository;
        }

        // GET: api/Assignmentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments()
        {
            try
            {
                var a = await service.GetAllAsync();
            return Ok(a);
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetAssignment(int id)
        {
            try
            {
                var a = await service.GetByIdAsync(id);
                if (a == null)
                    return NotFound($"No Assignmetn was found with Id: {id}");
                return Ok(a);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, AssignmentDTO dto)
        {
            try
            {
                var isValidCourseId = await courseService.IsValidCourseId(dto.CourseId);
                if (!isValidCourseId)
                    return BadRequest("Invalid CourseId!");

                var assignment = await service.GetByIdAsync(id);
                if (assignment == null) return NotFound($"No Assignment was found with Id: {id}");
                //var r = mapper.Map<Assignment>(dto);
                assignment.Title = dto.Title;
                assignment.Description = dto.Description;
                assignment.FilePath = dto.FilePath;
                assignment.StartDate = dto.StartDate;
                assignment.EndTime = dto.EndTime;
                assignment.Grade = dto.Grade;
                assignment.CourseId = dto.CourseId;
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
        [HttpPost]
        public async Task<ActionResult<Assignment>> AddAssignment(AssignmentDTO dto)
        {
            try
            {
                var isValidCourseId = await courseService.IsValidCourseId(dto.CourseId);
                if (!isValidCourseId)
                    return BadRequest("Invalid CourseId!");
                var r = mapper.Map<Assignment>(dto);
                return Ok(await service.AddAsync(r));
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
        [HttpDelete("{id}")]
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
                return Ok(a);
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
        [HttpGet("GetAssignmentsByCourseId/{courseId}")]
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
        [HttpGet("GetAssignmentsByCourseIdForStudent/{courseId}/{studentId}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentsByCourseIdForStudent(int courseId, int studentId)
        {
            try
            {
                var isValidCourseId = await courseService.IsValidCourseId(courseId);
                if (!isValidCourseId)
                    return BadRequest("Invalid CourseId!");
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                if(!isValidStudentId)
                    return BadRequest("Invalid StudentId!");
                var a = await service.GetAssignmentsByCourseId(courseId);
                if (!a.Any())
                    return NotFound($"No Assignments were found with CourseId: {courseId}");
                var assignments = mapper.Map<IEnumerable<AssignmentDetailsDTO>>(a);
                foreach (var i in assignments)
                {
                    i.Submitted = await assignmentAnswerRepository.IsSubmittedAssignmentAnswer(i.Id, studentId);
                    i.AssignedGrade = await assignmentGradeRepository.GetIntAssignmentGrade(i.Id, studentId);
                }  
                return Ok(assignments);
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
