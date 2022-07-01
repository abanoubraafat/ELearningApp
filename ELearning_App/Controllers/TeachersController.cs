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
using ELearning_App.Helpers;
using Serilog;

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private ITeacherRepository service { get; }
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment _host;
        public TeachersController(ITeacherRepository _service, IMapper mapper, IUserRepository userRepository, IWebHostEnvironment _host)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.userRepository = userRepository;
            this._host = _host;
        }

        // GET: api/Teachers
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllAsync());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: TeachersController , Action: GetTeachers , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        // GET: api/Teachers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Teacher>> GetTeacher(int id)
        //{
        //    try
        //    {
        //        if (service.GetById(id) == null)
        //            return NotFound();
        //        return Ok(await service.GetByIdAsync(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: TeachersController , Action: GetTeacher , Message: {ex.Message}");
        //        return NotFound();
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        //{
        //    try
        //    {
        //        if (id != teacher.Id)
        //        {
        //            return BadRequest();
        //        }
        //        return Ok(await service.Update(teacher));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: TeachersController , Action: PutTeacher , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher([FromForm] TeacherDTO dto)
        {
            try
            {
                var isNotAvailableUserEmail = await userRepository.IsNotAvailableUserEmail(dto.EmailAddress);
                if (isNotAvailableUserEmail) 
                    return BadRequest("There's already an account with the same Email address");
                else if (!dto.Role.Equals("Teacher"))
                    return BadRequest("Make sure the Role field is 'Teacher'");
                string hashedPassword = userRepository.CreatePasswordHash(dto.Password);
                dto.Password = hashedPassword.ToString();
                var teacher = mapper.Map<Teacher>(dto);
                if (dto.ProfilePic != null)
                {
                    if (!PicturesConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.ProfilePic.FileName).ToLower()))
                        return BadRequest("Only .png , .jpg and .jpeg images are allowed!");

                    if (dto.ProfilePic.Length > PicturesConstraints.maxAllowedSize)
                        return BadRequest("Max allowed size for profile picture is 5MB!");
                    var img = dto.ProfilePic;
                    var randomName = Guid.NewGuid() + Path.GetExtension(dto.ProfilePic.FileName);
                    var filePath = Path.Combine(_host.WebRootPath + "/Images", randomName);
                    using (FileStream fileStream = new(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(fileStream);
                    }
                    teacher.ProfilePic = @"\\Abanoub\wwwroot\Images\" + randomName;
                }
                await service.AddAsync(teacher);
                return Ok();

            }
            catch (Exception ex)
            {
                Log.Error($"Controller: TeachersController , Action: PostTeacher , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/Teachers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTeacher(int id)
        //{
        //    try
        //    {
        //        return Ok(await service.Delete(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: TeachersController , Action: DeleteTeacher , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("GetAllWithCourses")]
        //public async Task<ActionResult<IEnumerable<Teacher>>> GetAllWithCourses()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllWithCourses());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: TeachersController , Action: GetAllWithCourses , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("GetByIdWithCourses/{id}")]
        //public async Task<ActionResult<Teacher>> GetByIdWithCourses([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithCourses(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: TeachersController , Action: GetByIdWithCourses , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
    }
}
