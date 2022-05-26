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

        public TeachersController(ITeacherRepository _service)
        {
            service = _service;
            new Logger();
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
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            try
            {
                return Ok(await service.AddAsync(teacher));
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
