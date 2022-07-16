#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELearning_App.Domain.Entities;
using Microsoft.Extensions.Logging;
using ELearning_App.Helpers;
using Serilog;
using ELearning_App.Repository.UnitOfWork;

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private ICourseRepository service { get; }
        private readonly IMapper mapper;
        private readonly ITeacherRepository teacherRepository;
        IStudentRepository studentRepository;
        private readonly IWebHostEnvironment _host;
        public CoursesController(ICourseRepository _service, IMapper mapper, ITeacherRepository teacherRepository, IStudentRepository studentRepository, IWebHostEnvironment _host)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this._host = _host;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCourses , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            try
            {
                if (await service.GetByIdAsync(id) == null)
                    return NotFound($"No Course was found with that id: {id}");
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCourse , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, UpdateCourseDTO dto)
        {
            try
            {
                var isValidTeacherId = await teacherRepository.IsValidTeacherId(dto.TeacherId);
                if (!isValidTeacherId)
                    return BadRequest("No Teacher with that id");
                var course = await service.GetByIdAsync(id);
                if (course == null) return NotFound();
                if(dto.CourseImage != null && !dto.CourseImage.Equals(course.CourseImage))
                {
                    return BadRequest("for updating the picture use the specified endpoint for that");
                }
                course.CourseName = dto.CourseName;
                course.CourseDescription = dto.CourseDescription;
                //course.TeacherId = dto.TeacherId;
                return Ok(await service.Update(course));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: PutCourse , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse([FromForm] CourseDTO dto)
        {
            try
            {
                if (dto.Id != 0)
                    return BadRequest("Id is auto generated don't assign it.");
                var isValidTeacherId = await teacherRepository.IsValidTeacherId(dto.TeacherId);
                if(!isValidTeacherId)
                    return BadRequest("No Teacher with that id");
                var course = mapper.Map<Course>(dto);
                if (dto.CourseImage != null)
                {
                    if (!PicturesConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.CourseImage.FileName).ToLower()))
                        return BadRequest("Only .png , .jpg and .jpeg images are allowed!");

                    if (dto.CourseImage.Length > PicturesConstraints.maxAllowedSize)
                        return BadRequest("Max allowed size for pictures is 5MB!");
                    var img = dto.CourseImage;
                    var randomName = Guid.NewGuid() + Path.GetExtension(dto.CourseImage.FileName);
                    var filePath = Path.Combine(_host.WebRootPath + "/Images", randomName);
                    using (FileStream fileStream = new(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(fileStream);
                    }
                    course.CourseImage = randomName;
                }
                await service.AddAsync(course);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: PostCourse , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {

                var course = await service.GetByIdAsync(id);
                if (course == null) return NotFound();
                await service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: DeleteCourse , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("{courseId}/JoinCourse/{studentId}")]
        public async Task<ActionResult<Course>> JoinCourseForStudent(int studentId, int courseId)
        {
            try
            {
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                var isValidCourseId = await service.IsValidCourseId(courseId);
                if (!isValidStudentId)
                    return NotFound("Invalid studentId");
                else if (!isValidCourseId)
                    return NotFound("Invalid courseId");

                var added = await service.JoinCourseForStudent(studentId, courseId);
                if (added.Equals("Course Joined Succefully"))
                    return Ok();
                else if (added.Equals("Already Joined"))
                    return BadRequest(added);
                return BadRequest(added);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCourse , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpDelete("{courseId}/DropCourse/{studentId}")]
        public async Task<ActionResult<Course>> DropCourseForStudent(int studentId, int courseId)
        {
            try
            {
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                var isValidCourseId = await service.IsValidCourseId(courseId);
                if (!isValidStudentId)
                    return NotFound("Invalid studentId");
                else if (!isValidCourseId)
                    return NotFound("Invalid courseId");

                var dropped = await service.DropCourseForStudent(studentId, courseId);
                if (dropped.Equals("Dropped"))
                    return Ok();
                else
                    return BadRequest("Invalid studentId or courseId");
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCourse , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        #region Old Services
        //[HttpGet("GetAllWithTeachers")]
        //public async Task<ActionResult<IEnumerable<Course>>> GetAllWithTeachers()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllWithTeachers());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetAllWithTeachers , Message: {ex.Message}");
        //        return BadRequest();
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        //[HttpGet("GetAllWithStudents")]
        //public async Task<ActionResult<IEnumerable<Course>>> GetAllWithStudents()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllWithStudents());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetAllWithStudents , Message: {ex.Message}");
        //        return BadRequest();
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("Courses/{id}/Teachers")]
        //public async Task<ActionResult> GetByIdWithTeachers([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithTeachers(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetByIdWithTeachers , Message: {ex.Message}");
        //        return BadRequest();
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        //[HttpGet("GetByIdWithStudents/{id}")]
        //public async Task<ActionResult> GetByIdWithStudents([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithStudents(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: CoursesController , Action: GetByIdWithStudents , Message: {ex.Message}");
        //        return BadRequest();
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //} 
        #endregion
        // api/GetCoursesByTeacherId/5
        [HttpGet("GetCoursesByTeacherId/{teacherId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByTeacherId(int teacherId)
        {
            try
            {
                var isValidTeacherId = await teacherRepository.IsValidTeacherId(teacherId);
                if (!isValidTeacherId)
                    return BadRequest($"No Teacher with that id: {teacherId}");
                var courses = await service.GetCoursesByTeacherId(teacherId);
                //if (!courses.Any()) return NotFound("No Courses with that teacherId");
                return Ok(courses);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCoursesByTeacherId , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("GetCoursesByStudentId/{studentId}")]
        public async Task<ActionResult<IEnumerable<CourseDetailsDTO>>> GetCoursesByStudentId(int studentId)
        {
            try
            {
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                if (!isValidStudentId)
                    return BadRequest($"No Student with that id: {studentId}");
                var courses = await service.GetCoursesByStudentId(studentId);
                //if (!courses.Any()) return NotFound("No Courses with that studentId");
                var mapped = mapper.Map<IEnumerable<CourseDetailsDTO>>(courses);
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: GetCoursesByStudentId , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        #region Old Services 2
        //[HttpGet("Last5CoursesJoined/{studentId}")]
        //public async Task<ActionResult<IEnumerable<CourseDetailsDTO>>> Last5CoursesJoined(int studentId)
        //{
        //    //try
        //    //{
        //        var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
        //        if (!isValidStudentId) return BadRequest($"Invalid studentId :{studentId}");
        //        var last5CoursesJoined = await service.Last5CoursesJoined(studentId);
        //        //foreach(var course in last5CoursesJoined)
        //        //    if(course == null)
        //        var mapped = mapper.Map<CourseDetailsDTO>(last5CoursesJoined);
        //        return Ok(mapped);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Log.Error($"Controller: CoursesController , Action: Last5CoursesJoined , Message: {ex.Message}");
        //    //    return BadRequest();
        //    //}
        //    //finally
        //    //{
        //    //    Log.CloseAndFlush();
        //    //}
        //}
        //[HttpGet("Last5CoursesCreated/{teacherId}")]
        //public async Task<ActionResult<List<Course>>> Last5CoursesCreated(int teacherId)
        //{
        //    //try
        //    //{
        //    var isValidTeacherId = await teacherRepository.IsValidTeacherId(teacherId);
        //    if (!isValidTeacherId) return BadRequest($"Invalid teacherId :{teacherId}");
        //    var last5CoursesCreated = await service.Last5CoursesCreated(teacherId);
        //    //for (int i = 0; i < last5CoursesCreated.Count(); i++)
        //    //    if (last5CoursesCreated[i] == null)
        //    //        last5CoursesCreated.Remove(last5CoursesCreated[i]);
        //     return Ok(last5CoursesCreated);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Log.Error($"Controller: CoursesController , Action: Last5CoursesJoined , Message: {ex.Message}");
        //    //    return BadRequest();
        //    //}
        //    //finally
        //    //{
        //    //    Log.CloseAndFlush();
        //    //}
        //} 
        #endregion
        [HttpPut("update-photo/{id}")]
        public async Task<IActionResult> UpdateFile(int id, [FromForm] UpdateFileDTO dto)
        {
            try
            {
                var course = await service.GetByIdAsync(id);
                if (course == null) return NotFound($"No Course was found with Id: {id}");
                if (dto.File != null)
                {
                    if (!PicturesConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.File.FileName).ToLower()))
                        return BadRequest("Only .png , .jpg and .jpeg images are allowed!");

                    if (dto.File.Length > PicturesConstraints.maxAllowedSize)
                        return BadRequest("Max allowed size for pictures is 5MB!");
                    var img = dto.File;
                    var randomName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
                    var filePath = Path.Combine(_host.WebRootPath + "/Images", randomName);
                    using (FileStream fileStream = new(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(fileStream);
                    }
                    course.CourseImage = randomName;
                    return Ok(await service.Update(course));
                }
                else
                {
                    return BadRequest("photo field is required");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: UpdateFile , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpPut("form/{id}")]
        public async Task<IActionResult> PutCourseForm(int id,[FromForm] CourseDTO dto)
        {
            try
            {
                var isValidTeacherId = await teacherRepository.IsValidTeacherId(dto.TeacherId);
                if (!isValidTeacherId)
                    return BadRequest("No Teacher with that id");
                var course = await service.GetByIdAsync(id);
                if (course == null) return NotFound();
                if (dto.CourseImage != null)
                {
                    if (!PicturesConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.CourseImage.FileName).ToLower()))
                        return BadRequest("Only .png , .jpg and .jpeg images are allowed!");

                    if (dto.CourseImage.Length > PicturesConstraints.maxAllowedSize)
                        return BadRequest("Max allowed size for pictures is 5MB!");
                    var img = dto.CourseImage;
                    var randomName = Guid.NewGuid() + Path.GetExtension(dto.CourseImage.FileName);
                    var filePath = Path.Combine(_host.WebRootPath + "/Images", randomName);
                    using (FileStream fileStream = new(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(fileStream);
                    }
                    course.CourseImage = randomName;
                }
                course.CourseName = dto.CourseName;
                course.CourseDescription = dto.CourseDescription;
                return Ok(await service.Update(course));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: CoursesController , Action: PutCourse , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}
