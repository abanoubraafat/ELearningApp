using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseStudentController : ControllerBase
    {
        private readonly ICourseStudentRepository service;
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IMapper mapper;
        public CourseStudentController(ICourseStudentRepository service, IStudentRepository studentRepository, ICourseRepository courseRepository, ITeacherRepository teacherRepository, IMapper mapper)
        {
            this.service = service;
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.teacherRepository = teacherRepository;
            this.mapper = mapper;
            new Logger();
        }
        [HttpGet("GetUnVerifiedCourseStudentRequests/{teacherId}")]
        public async Task<ActionResult<IEnumerable<CourseStudentUnVerifiedRequestsDTO>>> GetUnVerifiedCourseStudentRequests(int teacherId)
        {
            try
            {
                var isValidTeacherId = await teacherRepository.IsValidTeacherId(teacherId);
                if (!isValidTeacherId) return BadRequest($"Invalid teacherId : {teacherId}");
                var requests = await service.GetUnVerifiedCourseStudentRequests(teacherId);
                var mappedRequests = mapper.Map<IEnumerable<CourseStudentUnVerifiedRequestsDTO>>(requests);
                return Ok(mappedRequests);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CourseStudentController , Action: GetUnVerifiedCourseStudentRequests , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
        [HttpPut("VerifyAddCourseToStudentRequest/{courseId}/{studentId}")]
        public async Task<ActionResult> VerifyAddCourseToStudentRequest(int courseId, int studentId)
        {
            try
            {
                var isValidCourseId = await courseRepository.IsValidCourseId(courseId);
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                if (!isValidCourseId) return BadRequest($"Invalid courseId : {courseId}");
                if (!isValidStudentId) return BadRequest($"Invalid studentID : {studentId}");
                var exsistingCourseStudentCompositeKey = await service.ExsistingCourseStudentCompositeKey(courseId, studentId);
                if (!exsistingCourseStudentCompositeKey) return BadRequest("Invalid CourseStudentId");
                await service.VerifyAddCourseToStudentRequest(courseId, studentId);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CourseStudentController , Action: VerifyAddCourseToStudentRequest , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
