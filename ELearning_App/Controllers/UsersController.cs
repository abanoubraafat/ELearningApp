#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELearning_App.Domain.Entities;
using ELearning_App.Helpers;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using ELearning_App.Repository.UnitOfWork;

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private IUserRepository service { get; }
        private readonly IStudentRepository studentService;
        private readonly ITeacherRepository teacherService;
        private readonly IParentRepository parentService;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment _host;
        public UsersController(IUserRepository _service, IMapper mapper, IStudentRepository studentService, ITeacherRepository teacherService, IParentRepository parentService, IWebHostEnvironment host)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.studentService = studentService;
            this.teacherService = teacherService;
            this.parentService = parentService;
            _host = host;
        }

        //GET: api/LoginInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: LoginInfoController , Action: GetLoginInfos , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        //GET: api/LoginInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                if (await service.GetByIdAsync(id) == null)
                    return NotFound();
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: LoginInfoController , Action: GetLoginInfo , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/LoginInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO dto)
        {

            try
            {
                var user = await service.GetByIdAsync(id);
                if (user == null)
                    return NotFound($"No User was found with this id: {id}");
                //else if (!(dto.Role.Equals("Student") || dto.Role.Equals("Parent") || dto.Role.Equals("Teacher")))
                //    return BadRequest("Role must be 'Student', 'Parent', or 'Teacher'");
                else if (!dto.Role.Equals(user.Role))
                    return BadRequest("Role field Can't be changed");
                else if (dto.EmailAddress.Equals(user.EmailAddress))
                {
                    user.FirstName = dto.FirstName;
                    user.LastName = dto.LastName;
                    //user.ProfilePic = dto.ProfilePic;
                    //user.EmailAddress = dto.EmailAddress;
                    if(dto.ProfilePic != null && !dto.ProfilePic.Equals(user.ProfilePic))
                        return BadRequest("for updating the picture use the specified endpoint for that");
                    if (!user.Password.Equals(dto.Password))
                        user.Password = service.CreatePasswordHash(dto.Password);
                    user.Phone = dto.Phone;
                    //user.Role = dto.Role;
                    return Ok(await service.Update(user));
                }
                else if (await service.IsNotAvailableUserEmail(dto.EmailAddress))
                    return BadRequest("There's already an account with the same Email address");
                else
                {
                    user.FirstName = dto.FirstName;
                    user.LastName = dto.LastName;
                    //user.ProfilePic = dto.ProfilePic;
                    if (dto.ProfilePic != null && !dto.ProfilePic.Equals(user.ProfilePic))
                        return BadRequest("for updating the picture use the specified endpoint for that");
                    user.EmailAddress = dto.EmailAddress;
                    if (!user.Password.Equals(dto.Password))
                        user.Password = service.CreatePasswordHash(dto.Password);
                    user.Phone = dto.Phone;
                    //user.Role = dto.Role;
                    return Ok(await service.Update(user));
                }

            }
            catch (Exception ex)
            {
                Log.Error($"Controller: LoginInfoController , Action: PutLoginInfo , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

            // POST: api/LoginInfoes
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            //[HttpPost]
            //public async Task<ActionResult<User>> AddUser(User user)
            //{
            //    //try
            //    //{
            //        //var addedUser = await service.AddAsync(user);
            //        //var loginInfo = new LoginInfo { emailAddress = dto.EmailAddress, password = dto.Password, type = dto.Type, ToDoLists = dto.ToDoLists};
            //        if (user == null) return BadRequest("The User can't be null!");
            //        //else if (user.Role.Equals("Student"))
            //        //    await studentService.AddAsync(new Student { Id = user.Id, EmailAddress = user.EmailAddress, Password = user.Password, FirstName = user.FirstName, LastName = user.LastName, ProfilePic = user.ProfilePic, Phone = user.Phone, Role = user.Role });
            //        //else if (user.Role.Equals("Teacher"))
            //        //    await teacherService.AddAsync(new Teacher { Id = user.Id });
            //        //else if (user.Role.Equals("Parent"))
            //        //    await parentService.AddAsync(new Parent { Id = user.Id });
            //        //else
            //        //    return BadRequest("Please specify the Role field CORRECTLY!!");
            //        return Ok(await service.AddAsync(user));
            //}
            //catch (Exception ex)
            //{
            //    Log.Error($"Controller: LoginInfoController , Action: PostLoginInfo , Message: {ex.Message}");
            //    return StatusCode(500);
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
            //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await service.GetByIdAsync(id);
                if (user == null) return NotFound($"Invalid userId : {id}");
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: LoginInfoController , Action: DeleteLoginInfo , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var ExistingEmailAdress = await service.IsNotAvailableUserEmail(loginRequest.EmailAddress);
                if (!ExistingEmailAdress)
                    return NotFound($"Invalid email : {loginRequest.EmailAddress}");
                if (await service.Login(loginRequest) == null)
                    return BadRequest($"Invalid password");
                var result = await service.Login(loginRequest);
                //if (!string.IsNullOrEmpty(result.RefreshToken))
                //    SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: LoginInfoController , Action: Login , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
        //refreshs the security token the front send if security token is expired
        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO model)
        {
            //var refreshToken = Request.Cookies["refreshToken"];
            var refreshToken = model.RefreshToken;
            var result = await service.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);
            //SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }
        //revoke the refresh token (if the user logged out for ex)
        [HttpPost("revokeRefreshToken")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenDTO model)
        {
            var token = model.RefreshToken;

            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");

            var result = await service.RevokeTokenAsync(token);

            if (!result)
                return BadRequest("Token is invalid!");

            return Ok();
        }

        [HttpPost("LoginTest")]
        public async Task<ActionResult<User>> LoginTest(LoginRequest loginRequest)
        {
            var ExistingEmailAdress = await service.IsNotAvailableUserEmail(loginRequest.EmailAddress);
            if (!ExistingEmailAdress)
                return NotFound($"Invalid email : {loginRequest.EmailAddress}");
            var user = await service.LoginTest(loginRequest);
            if (user == null)
                return BadRequest($"Invalid password");
            return Ok(user);

        }
        [HttpGet("EmailExists/{email}")]
        public async Task<ActionResult> EmailExists(string email)
        {
            var exist = await service.IsNotAvailableUserEmail(email);
            if (exist)
                return BadRequest();
            return Ok();
        }
        [HttpGet("GetByEmail/{email}")]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            return Ok(await service.GetByEmailAsync(email));
        }
        //[HttpPost("changePassword")]
        //public async Task<IActionResult> ChangePassword(string email, string oldPassword, string newPassword)
        //{
        //    try
        //    {
        //        var user = await service.GetByEmailAsync(email);
        //        if (user == null) return NotFound($"Invalid email : {email}");
        //        bool isValidPassword = service.VerifyPassword(oldPassword, user.Password);
        //        if (!isValidPassword)
        //            return BadRequest($"Invalid password");
        //        user.Password = newPassword;
        //        return Ok(await service.Update(user));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: LoginInfoController , Action: ChangePassword , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
        [HttpPut("update-photo/{id}")]
        public async Task<IActionResult> UpdateFile(int id, [FromForm] UpdateFileDTO dto)
        {
            try
            {
                var user = await service.GetByIdAsync(id);
                if (user == null) return NotFound($"No Course was found with Id: {id}");
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
                    user.ProfilePic = @"\\Abanoub\wwwroot\Images\" + randomName;
                    return Ok(await service.Update(user));
                }
                else
                {
                    return BadRequest("image can't be null;");
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