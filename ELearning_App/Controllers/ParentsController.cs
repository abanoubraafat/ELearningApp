﻿#nullable disable
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

        public ParentsController(IParentRepository _service)
        {
            service = _service;
            new Logger();
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
        public async Task<ActionResult<Parent>> PostParent(Parent parent)
        {
            try
            {
                return Ok(await service.AddAsync(parent));
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
    }
}
