using ELearning_App.Domain.Entities;
using ELearning_App.Helpers;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentAnswersController : ControllerBase
    {
        private IAssignmentAnswerRepository service { get; }
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AssignmentAnswersController(IAssignmentAnswerRepository _service, IAssignmentRepository assignmentRepository, IStudentRepository studentRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            service = _service;
            new Logger();
            this.assignmentRepository = assignmentRepository;
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        // GET: api/AssignmentAnsweres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentAnswer>>> GetAssignmentAnswers()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentAnswerController , Action: GetAssignmentAnswers , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/AssignmentAnsweres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentAnswer>> GetAssignmentAnswer(int id)
        {
            try
            {
                var a = await service.GetByIdAsync(id);
                if (a == null)
                    return NotFound();         
                return Ok(a);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentAnswerController , Action: GetAssignmentAnswer , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/AssignmentAnsweres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignmentAnswer(int id, [FromBody] AssignmentAnswerDTO a)
        {

            try
            {
                var isValidAssignmentId = await assignmentRepository.IsValidAssignmentId(a.AssignmentId);
                var isValidStudentId = await studentRepository.IsValidStudentId(a.StudentId);
                if (!isValidAssignmentId)
                    return BadRequest($"Invalid AssignmentId : {a.AssignmentId}");
                else if (!isValidStudentId)
                    return BadRequest($"Invalid StudentId : {a.StudentId}");
                var assignment = await service.GetByIdAsync(id);
                if (assignment == null) return NotFound($"No AssignmentAnswer was found with Id: {id}");
                //var aa = mapper.Map<AssignmentAnswer>(a);
                assignment.FileName = a.FileName;
                assignment.PDF = a.PDF;
                assignment.SubmitDate = a.SubmitDate;
                assignment.AssignmentId = a.AssignmentId;
                //assignment.StudentId = a.StudentId;
                return Ok(await service.Update(assignment));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentAnswerController , Action: PutAssignmentAnswer , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/AssignmentAnsweres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AssignmentAnswer>> PostAssignmentAnswer(AssignmentAnswerDTO a)
        {
            try
            {
                var isValidAssignmentId = await assignmentRepository.IsValidAssignmentId(a.AssignmentId);
                var isValidStudentId = await studentRepository.IsValidStudentId(a.StudentId);
                var isNotValidAssignmentAnswerWithStudentId = await service.IsNotValidAssignmentAnswerWithStudentId(a.StudentId, a.AssignmentId);
                if (!isValidAssignmentId)
                    return BadRequest($"Invalid AssignmentId : {a.AssignmentId}");
                else if (!isValidStudentId)
                    return BadRequest($"Invalid StudentId : {a.StudentId}");
                else if (isNotValidAssignmentAnswerWithStudentId)
                    return BadRequest($"There's already an existing Assignment Answer with this StudentId :{a.StudentId}, to this assignment");
                var aa = mapper.Map<AssignmentAnswer>(a);
                return Ok(await service.AddAsync(aa));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentAnswerController , Action: PostAssignmentAnswer , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/AssignmentAnsweres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignmentAnswer(int id)
        {
            try
            {
                var assignment = await service.GetByIdAsync(id);
                if (assignment == null)
                    return NotFound($"No AssignmentAnswer was found with Id: {id}");
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentAnswerController , Action: DeleteAssignmentAnswer , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("GetAssignmentAnswersByAssignmentId/{assignmentId}")]
        public async Task<ActionResult<IEnumerable<AssignmentAnswerDetailsDTO>>> GetAssignmentAnswersByAssignmentId(int assignmentId)
        {
            try
            {
                var isValidAssignmentId = await assignmentRepository.IsValidAssignmentId(assignmentId);
                if (!isValidAssignmentId)
                    return BadRequest("Invalid AssignmentId!");
                var a = await service.GetAssignmentAnswersByAssignmentId(assignmentId);
                if (a.Count() == 0)
                    return NotFound($"No Assignment was found with Id: {assignmentId}");
                var b = mapper.Map<IEnumerable<AssignmentAnswerDetailsDTO>>(a);
                return Ok(b);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentAnswerController , Action: GetAssignmentAnswersByAssignmentId , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("GetAssignmentAnswerByStudentIdByAssignmentId/{studentId}/{assignmentId}")]
        public async Task<ActionResult<AssignmentAnswer>> GetAssignmentAnswerByStudentIdByAssignmentId(int studentId, int assignmentId)
        {
            try
            {
                var isValidAssignmentId = await assignmentRepository.IsValidAssignmentId(assignmentId);
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                if (!isValidAssignmentId)
                    return BadRequest($"Invalid AssignmentId : {assignmentId}");
                else if (!isValidStudentId)
                    return BadRequest($"Invalid StudentId : {studentId}");
                var a = await service.GetAssignmentAnswerByStudentIdByAssignmentId(studentId, assignmentId);
                if (a == null)
                    return NotFound($"No Assignment Associated with this Student was found");
                return Ok(a);
            }
            catch(System.InvalidOperationException)
            {
                return BadRequest("There're more than answer to this assignment by this student");
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentAnswerController , Action: GetAssignmentAnswerByStudentIdByAssignmentId , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        //// GET: api/AssignmentAnsweres
        //[HttpGet("GetNotGradedAnswers")]
        //public async Task<ActionResult<IEnumerable<AssignmentAnswer>>> GetNotGradedAnswers()
        //{
        //    try
        //    {
        //        return Ok(await service.GetNotGradedAnswers());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: AssignmentAnswerController , Action: GetNotGradedAnswers , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("GetByIdWithGrade/{id}")]
        //public async Task<ActionResult<AssignmentAnswer>> GetByIdWithGrade(int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithGrade(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: AssignmentAnswerController , Action: GetByIdWithGrade , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("GetByIdWithFeedback/{id}")]
        //public async Task<ActionResult<AssignmentAnswer>> GetByIdWithFeedback(int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithFeedback(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: AssignmentAnswerController , Action: GetByIdWithFeedback , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("GetByIdWithBadge/{id}")]
        //public async Task<ActionResult<AssignmentAnswer>> GetByIdWithBadge(int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithBadge(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: AssignmentAnswerController , Action: GetByIdWithBadge , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}


    }
}
