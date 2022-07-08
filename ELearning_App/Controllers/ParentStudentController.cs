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
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
