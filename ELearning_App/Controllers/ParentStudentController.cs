using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositories;

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentStudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IParentRepository _parentRepository;
        private readonly IParentStudentRepository service;
        private readonly IMapper mapper;
        public ParentStudentController(IStudentRepository studentRepository, IParentRepository parentRepository, IParentStudentRepository service, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
            this.service = service;
            this.mapper = mapper;
            new Logger();

        }
        [HttpPost]
        public async Task<ActionResult<ParentStudent>> PostParentStudent(ParentStudentDTO dto)
        {
            try
            {
                var parentStudent = new ParentStudent();
                var isValidParentId = await _parentRepository.IsValidParentId(dto.ParentId);
                if (!isValidParentId)
                    return BadRequest($"Invalid parentId : {dto.ParentId}");
                var student = await _studentRepository.GetStudentByEmail(dto.StudentEmail);
                if (student == null) return BadRequest("Invalid Email");
                var exsistingParentStudentCompositeKey = await service.ExsistingParentStudentCompositeKey(dto.ParentId, student.Id);
                if (exsistingParentStudentCompositeKey) return BadRequest("This student is already added to this parent");
                parentStudent.StudentId = student.Id;
                parentStudent.ParentId = dto.ParentId;
                await service.AddAsync(parentStudent);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ParentStudentController , Action: PostParentStudent , Message: {ex.Message}");
                return StatusCode(404);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("GetUnVerifiedParentStudentRequests/{studentId}")]
        public async Task<ActionResult<IEnumerable<ParentStudentsUnVerifiedRequestDTO>>> GetUnVerifiedParentStudentRequests(int studentId)
        {
            try
            {
                var isValidStudentId = await _studentRepository.IsValidStudentId(studentId);
                if (!isValidStudentId) return BadRequest($"Invalid studentId : {studentId}");
                var requests = await service.GetUnVerifiedParentStudentRequests(studentId);
                var mappedRequests = mapper.Map<IEnumerable<ParentStudentsUnVerifiedRequestDTO>>(requests);
                return Ok(mappedRequests);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ParentStudentController , Action: GetUnVerifiedParentStudentRequests , Message: {ex.Message}");
                return StatusCode(404);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpPut("VerifyParentStudentRequest/{parentId}/{studentId}")]
        public async Task<ActionResult> VerifyAddParentToStudentRequest(int parentId, int studentId)
        {
            try
            {
                var isValidStudentId = await _studentRepository.IsValidStudentId(studentId);
                if (!isValidStudentId) return BadRequest($"Invalid studentId : {studentId}");
                var isValidParentId = await _parentRepository.IsValidParentId(parentId);
                if (!isValidParentId) return BadRequest($"Invalid parentId : {parentId}");
                var exsistingParentStudentCompositeKey = await service.ExsistingParentStudentCompositeKey(parentId, studentId);
                if (!exsistingParentStudentCompositeKey) return BadRequest("Invalid ParentStudentId");
                await service.VerifyAddParentToStudentRequest(parentId, studentId);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ParentStudentController , Action: VerifyAddParentToStudentRequest , Message: {ex.Message}");
                return StatusCode(404);
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
        //[HttpPost("AddMultipleParentStudentReq")]
        //public async Task<ActionResult> PostMultipleParentStudentReq(List<ParentStudentDTO> parentStudentDTO)
        //{
        //    List<ParentStudent> parentStudentList = new List<ParentStudent>();
        //    for (int i = 0; i < parentStudentDTO.Count(); i++)
        //    {
        //        var isValidParentID = await _parentRepository.IsValidParentId(parentStudentDTO[i].ParentId);
        //        var student = await _studentRepository.GetStudentByEmail(parentStudentDTO[i].StudentEmail);
        //        var hasStudent = _parentRepository.HasStudent(parentStudentDTO[i].ParentId, student.Id);
        //        if (isValidParentID && student != null && hasStudent.Equals("No"))
        //        { 
        //            parentStudentList.Add(new ParentStudent { ParentId = parentStudentDTO[i].ParentId, StudentId = student.Id });   
        //        }
        //    }
        //    await service.AddMultipleAsync(parentStudentList);
        //    return Ok();
        //}
    }
}
