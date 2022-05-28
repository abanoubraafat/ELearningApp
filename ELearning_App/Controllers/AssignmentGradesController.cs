﻿using System;
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
    [Route("api/")]
    [ApiController]
    public class AssignmentGradesController : ControllerBase
    {
        private IAssignmentGradeRepository service { get; }
        private readonly IAssignmentAnswerRepository assignmentAnswerRepository;
        private readonly IMapper mapper;
        public AssignmentGradesController(IAssignmentGradeRepository _service, IAssignmentAnswerRepository assignmentAnswerRepository, IMapper mapper)
        {
            service = _service;
            new Logger();
            this.assignmentAnswerRepository = assignmentAnswerRepository;
            this.mapper = mapper;
        }

        // GET: api/AssignmentGradees
        [HttpGet("AssignmentGrades")]
        public async Task<ActionResult<IEnumerable<AssignmentGrade>>> GetAssignmentGrades()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentGradeController , Action: GetAssignmentGrades , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/AssignmentGradees/5
        [HttpGet("AssignmentGrades/{id}")]
        public async Task<ActionResult<AssignmentGrade>> GetAssignmentGrade(int id)
        {
            try
            {
                if (service.GetByIdAsync(id) == null)
                    return NotFound();
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentGradeController , Action: GetAssignmentGrade , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/AssignmentGradees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("AssignmentGrades/{id}")]
        public async Task<IActionResult> PutAssignmentGrade(int id, [FromBody] AssignmentGradeDTO dto)
        {

            try
            {
                var isValidAssignmentId = await assignmentAnswerRepository.IsValidAssignmentAnswerId(dto.AssignmentAnswerId);
                if (!isValidAssignmentId)
                    return BadRequest("Invalid AssignmentGradeId!");
                var assignment = await service.GetByIdAsync(id);
                if (assignment == null) return NotFound($"No AssignmentGrade was found with Id: {id}");
                var a = mapper.Map<AssignmentGrade>(dto);
                return Ok(await service.Update(a));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentGradeController , Action: PutAssignmentGrade , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/AssignmentGradees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AssignmentGrades")]
        public async Task<ActionResult<AssignmentGrade>> PostAssignmentGrade(AssignmentGradeDTO dto)
        {
            try
            {
                var isValidAssignmentId = await assignmentAnswerRepository.IsValidAssignmentAnswerId(dto.AssignmentAnswerId);
                if (!isValidAssignmentId)
                    return BadRequest("Invalid AssignmentGradeId!");
                var a = mapper.Map<AssignmentGrade>(dto);
                return Ok(await service.AddAsync(a));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentGradeController , Action: PostAssignmentGrade , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/AssignmentGradees/5
        [HttpDelete("AssignmentGrades/{id}")]
        public async Task<IActionResult> DeleteAssignmentGrade(int id)
        {
            try
            {
                var assignment = await service.GetByIdAsync(id);
                if (assignment == null) return NotFound($"No AssignmentGrade was found with Id: {id}");
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentGradeController , Action: DeleteAssignmentGrade , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("AssignmnetAnswers/{assignmentAnswerId}/AssignmentGrade")]
        public async Task<ActionResult<AssignmentGrade>> GetAssignmentGradeByAssignmentAnswerId(int assignmentAnswerId)
        {
            try
            {
                var isValidAssignmentId = await assignmentAnswerRepository.IsValidAssignmentAnswerId(assignmentAnswerId);
                if (!isValidAssignmentId)
                    return BadRequest("Invalid AssignmentAnswerId!");
                var a = await service.GetAssignmentGradeByAssignmentAnswerId(assignmentAnswerId);
                if (a == null)
                    return NotFound($"No AssignmentAnswer was found with Id: {assignmentAnswerId}");
                return Ok(a);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentGradeController , Action: GetAssignmentGrades , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
