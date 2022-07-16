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
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoList(int id, ToDoListDTO dto)
        {

            try
            {
                var isValidUserId = await userRepository.IsValidUserId(dto.UserId);
                if (!isValidUserId) return BadRequest("Invalid UserId");
                var toDoList = await service.GetByIdAsync(id);
                if (toDoList == null) return NotFound("Invalid ToDoListId");
                toDoList.Notes = dto.Notes;
                toDoList.Title = dto.Title;
                toDoList.Description = dto.Description;
                toDoList.Date = dto.Date;
                toDoList.Done = dto.Done;
                toDoList.Urgent = dto.Urgent;
                toDoList.Important = dto.Important;
                toDoList.UserId = dto.UserId;
                return Ok(await service.Update(toDoList));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ToDoListController , Action: PutToDoList , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ToDoList>> PostToDoList(ToDoListDTO dto)
        {
            try
            {
                if (dto.Id != 0)
                    return BadRequest("Id is auto generated don't assign it.");
                var isValidUserId = await userRepository.IsValidUserId(dto.UserId);
            if (!isValidUserId) return BadRequest("Invalid UserId");
            var t = mapper.Map<ToDoList>(dto);
                await service.AddAsync(t);
            return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ToDoListController , Action: PostToDoList , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoList(int id)
        {
            try
            {
                var toDoList = await service.GetByIdAsync(id);
                if (toDoList == null) return NotFound($"Invalid ToDoListId : {id}");
                await service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ToDoListController , Action: DeleteToDoList , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        
        [HttpGet("GetToDoListsByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<ToDoList>>> GetToDoListsByUserId(int userId)
        {
            try
            {
                var isValidUserId = await userRepository.IsValidUserId(userId);
                if (!isValidUserId) return BadRequest($"Invalid UserId {userId}");
                var toDoLists = await service.GetToDoListsByUserId(userId);
                //if (toDoLists == null) return NotFound($"No ToDoLists found for this user : {userId}");
                return Ok(toDoLists);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ToDoListController , Action: GetToDoLists , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
