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
        private readonly IWebHostEnvironment _host;
        public AssignmentsController(IAssignmentRepository _service, IMapper mapper, ICourseRepository courseService, IAssignmentAnswerRepository assignmentAnswerRepository, IStudentRepository studentRepository, IWebHostEnvironment host)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.courseService = courseService;
            this.assignmentAnswerRepository = assignmentAnswerRepository;
            this.studentRepository = studentRepository;
            _host = host;

        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, UpdateAssignmentDTO dto)
        {
            try
            {
                var isValidCourseId = await courseService.IsValidCourseId(dto.CourseId);
                if (!isValidCourseId)
                    return BadRequest("Invalid CourseId!");
                var assignment = await service.GetByIdAsync(id);
                if (assignment == null) return NotFound($"No Assignment was found with Id: {id}");
                assignment.Title = dto.Title;
                assignment.Description = dto.Description;
                if(dto.FilePath !=null && !dto.FilePath.Equals(assignment.FilePath))
                {
                    return BadRequest("for updating the file use the specified endpoint for that");
                }
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

        [HttpPost]
        public async Task<ActionResult<Assignment>> AddAssignment([FromForm] AssignmentDTO dto)
        {
            try
            {
                var isValidCourseId = await courseService.IsValidCourseId(dto.CourseId);
                if (!isValidCourseId)
                    return BadRequest("Invalid CourseId!");
                var r = mapper.Map<Assignment>(dto);
                if (dto.FilePath != null)
                {
                    if (!FilesConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.FilePath.FileName).ToLower()))
                        return BadRequest("Only .pdf, .doc, .docx, .ppt, .pptx, .xlsx, .rar, .zip, .png, .jpg, .jpeg and .txt files are allowed!");
                    var img = dto.FilePath;
                    var randomName = Guid.NewGuid() + Path.GetExtension(dto.FilePath.FileName);
                    var filePath = Path.Combine(_host.WebRootPath + "/Files", randomName);
                    using (FileStream fileStream = new(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(fileStream);
                    }
                    r.FilePath = @"\\Abanoub\wwwroot\Files\" + randomName;
                }
                await service.AddAsync(r);
                return Ok();

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            try
            {
                var assignment = await service.GetByIdAsync(id);
                if (assignment == null)
                    return NotFound($"No Assignment was found with Id: {id}");
                await service.Delete(id);
                return Ok();
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
        public async Task<ActionResult<IEnumerable<AssignmentDetailsDTO>>> GetAssignmentsByCourseIdForStudent(int courseId, int studentId)
        {
            try
            {
                var isValidCourseId = await courseService.IsValidCourseId(courseId);
                if (!isValidCourseId)
                    return BadRequest("Invalid CourseId!");
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                if (!isValidStudentId)
                    return BadRequest("Invalid StudentId!");
                var a = await service.GetAssignmentsByCourseIdForStudent(courseId);
                if (!a.Any())
                    return NotFound($"No Assignments were found with CourseId: {courseId}");
                var assignments = mapper.Map<IEnumerable<AssignmentDetailsDTO>>(a);
                foreach (var i in assignments)
                {
                    i.Submitted = await assignmentAnswerRepository.IsSubmittedAssignmentAnswer(i.Id, studentId);
                    i.AssignedGrade = await assignmentAnswerRepository.GetIntAssignmentGrade(i.Id, studentId);
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
        [HttpPut(template: "update-file/{id}")]
        public async Task<IActionResult> UpdateFile(int id, [FromForm] UpdateFileDTO dto)
        {
            try
            {
                var assignment = await service.GetByIdAsync(id);
                if (assignment == null) return NotFound($"No Assignment was found with Id: {id}");
                if (dto.File != null)
                {
                    if (!FilesConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.File.FileName).ToLower()))
                        return BadRequest("Only .pdf, .doc, .docx, .ppt, .pptx, .xlsx, .rar, .zip, .png, .jpg, .jpeg and .txt files are allowed!");
                    var img = dto.File;
                    var randomName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
                    var filePath = Path.Combine(_host.WebRootPath + "/Files", randomName);
                    using (FileStream fileStream = new(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(fileStream);
                    }
                    assignment.FilePath = @"\\Abanoub\wwwroot\Files\" + randomName;
                    return Ok(await service.Update(assignment));
                }
                else
                {
                    return BadRequest("file can't be null;");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentController , Action: UpdateFile , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
