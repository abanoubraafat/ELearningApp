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

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private IToDoListRepository service { get; }
        private readonly IMapper mapper;
        private IUserRepository userRepository;
        public ToDoListsController(IToDoListRepository _service, IMapper mapper, IUserRepository userRepository)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        // GET: api/ToDoListes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoList>>> GetToDoLists()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ToDoListController , Action: GetToDoLists , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/ToDoListes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoList>> GetToDoList(int id)
        {
            try
            {
                if (await service.GetByIdAsync(id) == null)
                    return NotFound();
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ToDoListController , Action: GetToDoList , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/ToDoListes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoList(int id, ToDoList toDoList)
        {

            try
            {
                if (id != toDoList.Id)
                {
                    return BadRequest();
                }
                return Ok(await service.Update(toDoList));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ToDoListController , Action: PutToDoList , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/ToDoListes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoList>> PostToDoList(ToDoList toDoList)
        {
            try
            {
                return Ok(await service.AddAsync(toDoList));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ToDoListController , Action: PostToDoList , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/ToDoListes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoList(int id)
        {
            try
            {
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ToDoListController , Action: DeleteToDoList , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        //[HttpPost("AddOne")]
        //public async Task<IActionResult> AddOne()
        //{
        //    var book = service.AddToDoList(new ToDoList { Notes ="", Date = new DateTime().Date, Time = new DateTime().Date, Done = true, Urgent=false, important = false, LoginInfoId = 5 });
        //    return Ok(await book);
        //}
    }
}
