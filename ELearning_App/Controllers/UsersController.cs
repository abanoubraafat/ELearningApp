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

        public UsersController(IUserRepository _service, IMapper mapper, IStudentRepository studentService, ITeacherRepository teacherService, IParentRepository parentService)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.studentService = studentService;
            this.teacherService = teacherService;
            this.parentService = parentService;
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
        public async Task<IActionResult> UpdateUser(int id, User loginInfo)
        {

            try
            {
                if (id != loginInfo.Id)
                {
                    return BadRequest();
                }
                return Ok(await service.Update(loginInfo));
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
        public async Task<IActionResult> DeleteLoginInfo(int id)
        {
            try
            {
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
        [HttpPost("login/{email}/{password}")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                if (await service.Login(email, password) == null)
                    return StatusCode(404);
                else
                    return Ok(await service.Login(email, password));
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
    }
}

// DELETE: api/LoginInfoes/5

//        [HttpGet("GetByIdWithToDoLists/{id}")]
//        public async Task<ActionResult<LoginInfo>> GetByIdWithToDoLists(int id)
//        {
//            try
//            {
//                return Ok(await service.GetByIdWithToDoLists(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: LoginInfoController , Action: GetByIdWithToDoLists , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }
//        //[HttpPost("AddOne")]
//        //public async Task<IActionResult> AddOne()
//        //{
//        //    var book = service.AddLoginInfo(new LoginInfo {emailAddress = "a@b.com", password ="123", type = 1 });
//        //    return  Ok(await book);
//        //}

//    }
//}
