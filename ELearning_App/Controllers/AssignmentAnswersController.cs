using ELearning_App.Domain.Entities;
using ELearning_App.Helpers;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ELearning_App.Controllers
{
    //[Authorize/*(Roles = "Teacher")*/]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentAnswersController : ControllerBase
    {
        private IAssignmentAnswerRepository service { get; }
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment _host;
        public AssignmentAnswersController(IAssignmentAnswerRepository _service, IAssignmentRepository assignmentRepository, IStudentRepository studentRepository, IMapper mapper, IWebHostEnvironment host)
        {
            service = _service;
            new Logger();
            this.assignmentRepository = assignmentRepository;
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            _host = host;
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignmentAnswer(int id, [FromBody] UpdateAssignmentAnswerDTO a)
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
                //assignment.PDF = a.PDF;
                if (a.PDF != null && !a.PDF.Equals(assignment.PDF))
                {
                    return BadRequest("for updating the file use the specified endpoint for that");
                }
                assignment.SubmitDate = a.SubmitDate;
                assignment.AssignmentId = a.AssignmentId;
                assignment.AssignedGrade = a.AssignedGrade;
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

        [HttpPost]
        public async Task<ActionResult<AssignmentAnswer>> PostAssignmentAnswer([FromForm] AssignmentAnswerDTO a)
        {
            try
            {
                var isValidAssignmentId = await assignmentRepository.IsValidAssignmentId(a.AssignmentId);
                var isValidStudentId = await studentRepository.IsValidStudentId(a.StudentId);
                var isNotValidAssignmentAnswerWithStudentId = await service.IsNotValidAssignmentAnswerWithStudentId(a.StudentId, a.AssignmentId);
                if (!isValidAssignmentId)
                    return BadRequest($"Invalid AssignmentId : {a.AssignmentId}");
                if (!isValidStudentId)
                    return BadRequest($"Invalid StudentId : {a.StudentId}");
                if (isNotValidAssignmentAnswerWithStudentId)
                    return BadRequest($"There's already an existing Assignment Answer with this StudentId :{a.StudentId}, to this assignment");

                if (!FilesConstraints.allowedExtenstions.Contains(Path.GetExtension(a.PDF.FileName).ToLower()))
                    return BadRequest("Only .pdf, .doc, .docx, .ppt, .pptx, .xlsx, .rar, .zip, .png, .jpg, .jpeg and .txt files are allowed!");
                a.AssignedGrade = null;
                var aa = mapper.Map<AssignmentAnswer>(a);
                var img = a.PDF;
                var randomName = Guid.NewGuid() + Path.GetExtension(a.PDF.FileName);
                var filePath = Path.Combine(_host.WebRootPath + "/Files", randomName);
                using (FileStream fileStream = new(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }
                aa.PDF = @"\\Abanoub\wwwroot\Files\" + randomName;
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

        [HttpPut("UpdateAssignmentAnswersAssignedGrade")]
        public async Task<IActionResult> UpdateAssignmentAnswersAssignedGrade([FromQuery] int[] ids, [FromBody] List<UpdateAssignmentAnswerDTO> a)
        {

            try
            {
                if (ids.Length != a.Count)
                    return BadRequest("Number of Ids must be equal to number of updates objects");
                var assignmentAnswers = await service.GetAssignmentAnswersByListOfIds(ids);
                for(int i = 0; i< assignmentAnswers.Count; i++)
                {
                    if (assignmentAnswers[i] == null)
                        return NotFound($"Invalid id :{ids[i]}");
                    var isValidAssignmentId = await assignmentRepository.IsValidAssignmentId(assignmentAnswers[i].AssignmentId);
                    var isValidStudentId = await studentRepository.IsValidStudentId(assignmentAnswers[i].StudentId);
                    if (!isValidAssignmentId)
                        return BadRequest($"Invalid AssignmentId : {assignmentAnswers[i].AssignmentId}");
                    else if (!isValidStudentId)
                        return BadRequest($"Invalid StudentId : {assignmentAnswers[i].StudentId}");
                    assignmentAnswers[i].FileName = a[i].FileName;
                    if (a[i].PDF != null && !a[i].PDF.Equals(assignmentAnswers[i].PDF))
                    {
                        return BadRequest("for updating the file use the specified endpoint for that");
                    }
                    assignmentAnswers[i].SubmitDate = a[i].SubmitDate;
                    assignmentAnswers[i].AssignmentId = a[i].AssignmentId;
                    assignmentAnswers[i].AssignedGrade = a[i].AssignedGrade;
                }
               
                return Ok(await service.UpdateMultiple(assignmentAnswers));
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
        [HttpPut(template: "update-file/{id}")]
        public async Task<IActionResult> UpdateFile(int id, [FromForm] UpdateFileDTO dto)
        {
            try
            {
                var assignmentAnswer = await service.GetByIdAsync(id);
                if (assignmentAnswer == null) return NotFound($"No Assignment was found with Id: {id}");
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
                    assignmentAnswer.PDF = @"\\Abanoub\wwwroot\Files\" + randomName;
                    return Ok(await service.Update(assignmentAnswer));
                }
                else
                {
                    return BadRequest("file can't be null;");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentAnswersController , Action: UpdateFile , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpPut("Add/Update/AssignmentAnswers/MultipleAssignedGrades")]
        public async Task<IActionResult> UpdateAssignmentAnswersMultipleAssignedGrades(MultipleAssignedGradeSetterDTO dto)
        {

            try
            {
                if (dto.Ids.Length != dto.AssignedGrades.Length)
                    return BadRequest("Number of Ids must be equal to number of updates objects");
                var assignmentAnswers = await service.GetAssignmentAnswersByListOfIds(dto.Ids);
                if (assignmentAnswers == null)
                    return NotFound("No AssignmentAnswers was found with these Ids");
                if (assignmentAnswers.Count != dto.AssignedGrades.Length)
                    return BadRequest("Found AssignmentAnswers are not equal to assignedGrades");
                for (int i = 0; i < assignmentAnswers.Count; i++)
                {
                    assignmentAnswers[i].AssignedGrade = dto.AssignedGrades[i];
                }

                return Ok(await service.UpdateMultiple(assignmentAnswers));
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
        [HttpPut("Add/Update/AssignmentAnswers/AssignedGrade")]
        public async Task<IActionResult> UpdateAssignmentAnswersAssignedGrades(AssignedGradeSetterDTO dto)
        {

            try
            {
                var answer = service.GetById(dto.Id);
                if (answer == null) return NotFound($"Invalid assignmentId : {dto.Id}");
                answer.AssignedGrade = dto.AssignedGrade;

                return Ok(await service.Update(answer));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentAnswerController , Action: UpdateAssignmentAnswersAssignedGrades , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
