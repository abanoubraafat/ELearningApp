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
    public class ParentsController : ControllerBase
    {
        private IParentRepository service { get; }
        private IStudentRepository studentRepository { get; }
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment _host;

        public ParentsController(IParentRepository _service, IStudentRepository studentRepository, IMapper mapper, IUserRepository userRepository, IWebHostEnvironment _host)
        {
            service = _service;
            new Logger();
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this._host = _host;
        }

        // GET: api/Parents
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Parent>>> GetParents()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllAsync());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: ParentsController , Action: GetParents , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        // GET: api/Parents/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Parent>> GetParent(int id)
        //{
        //    try
        //    {
        //        if (await service.GetByIdAsync(id) == null)
        //            return NotFound();
        //        return Ok(await service.GetByIdAsync(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: ParentsController , Action: GetParent , Message: {ex.Message}");
        //        return NotFound();
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        // PUT: api/Parents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutParent(int id, Parent parent)
        //{
        //    try
        //    {
        //        if (id != parent.Id)
        //        {
        //            return BadRequest();
        //        }
        //        return Ok(await service.Update(parent));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: ParentsController , Action: PutParent , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        // POST: api/Parents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Parent>> PostParent([FromForm] ParentDTO dto)
        {
            try
            {
                var isNotAvailableUserEmail = await userRepository.IsNotAvailableUserEmail(dto.EmailAddress);
                if (isNotAvailableUserEmail)
                    return BadRequest("There's already an account with the same Email address");
                else if (!dto.Role.Equals("Parent"))
                    return BadRequest("Make sure the Role field is 'Parent'");
                string hashedPassword = userRepository.CreatePasswordHash(dto.Password);
                dto.Password = hashedPassword;
                var parent = mapper.Map<Parent>(dto);
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
                    parent.ProfilePic = @"\\Abanoub\wwwroot\Images\" + randomName;
                    return Ok(await service.AddAsync(parent));
                }
                else
                {
                    return Ok(await service.AddAsync(parent));
                }
                    
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ParentsController , Action: PostParent , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("{parentId}/AddStudentsByEmailToParent/{studentEmail}")]
        public async Task<ActionResult<Parent>> AddStudentsByEmailToParent(int parentId, string studentEmail)
        {
            try
            {
                var isValidParentId = await service.IsValidParentId(parentId);
                if (!isValidParentId)
                    return NotFound("Invalid parentId");
                var isValidStudentEmail = await studentRepository.IsValidStudentEmail(studentEmail);
                if (!isValidStudentEmail)
                    return NotFound("Invalid studentEmail");
                var added = await service.AddStudentsByEmailToParent(parentId, studentEmail);
                if (added.Equals("Added"))
                    return Ok(added);
                else
                    return BadRequest(added);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ParentsController , Action: GetParents , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        //public async Task<ActionResult<Parent>> AddStudentsByEmail(int parentId, string studentEmail)
        //{
        //    var student = await studentRepository.FindAllAsync(s => s.EmailAddress == studentEmail);
        //    var parent = await service.GetByIdAsync(parentId);
        //    if (student == null || parent == null)
        //        return NotFound();
        //    parent.Students.Add(student);
        //    await service.Update(parent);
        //    return Ok();
        //}

        // DELETE: api/Parents/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteParent(int id)
        //{
        //    try
        //    {
        //        return Ok(await service.Delete(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: ParentsController , Action: DeleteParent , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        //[HttpGet("GetAllWithStudentWithCoursesWithGrades")]
        //public async Task<ActionResult<IEnumerable<Parent>>> GetAllWithStudentWithCoursesWithGrades()
        //{
        //    try
        //    {
        //        return Ok(await service.GetAllWithStudentWithCoursesWithGrades());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: ParentsController , Action: GetAllWithStudentWithCoursesWithGrades , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        //[HttpGet("GetByIdWithStudentWithCoursesWithGrades/{id}")]
        //public async Task<ActionResult<Parent>> GetByIdWithStudentWithCoursesWithGrades([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithStudentWithCoursesWithGrades(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: ParentsController , Action: GetByIdWithStudentWithCoursesWithGrades , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        //var imagePath = @"\Upload\Images\";
        //var uploadPath = _env.WebRootPath + imagePath;
        //if (!Directory.Exists(uploadPath))
        //    Directory.CreateDirectory(uploadPath);
        //var uniqFileName = Guid.NewGuid().ToString();
        //var fileName = Path.GetFileName(uniqFileName + "." + dto.ProfilePic.FileName.Split(".")[1].ToLower());
        //string fullPath = uploadPath + fileName;
        //imagePath += @"\";
        //var filePath = @"..\" + Path.Combine(imagePath, fileName);
        //using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //{
        //    await dto.ProfilePic.CopyToAsync(fileStream);
        //}
        //using var dataStream = new MemoryStream();

        //await dto.ProfilePic.CopyToAsync(dataStream);
        //string imagePath = Path.GetFileName(dto.ProfilePic.FileName);
        //dto.ProfilePic.SaveAs()
        //mapped.ProfilePic = fullPath;  

    }
}
