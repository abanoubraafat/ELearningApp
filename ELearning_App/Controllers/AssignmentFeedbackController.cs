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
    public class AssignmentFeedbackController : ControllerBase
    {
        private IAssignmentFeedbackRepository service { get; }
        //private IAssignmentAnswerService service2 { get; }
        public AssignmentFeedbackController(IAssignmentFeedbackRepository _service)
        {
            service = _service;
            //service2 = _service2;
            new Logger();
        }

        // GET: api/AssignmentFeedbackes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentFeedback>>> GetAssignmentFeedbacks()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentFeedbackController , Action: GetAssignmentFeedbacks , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/AssignmentFeedbackes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentFeedback>> GetAssignmentFeedback(int id)
        {
            try
            {
                if (service.GetByIdAsync(id) == null)
                    return NotFound();
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentFeedbackController , Action: GetAssignmentFeedback , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/AssignmentFeedbackes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignmentFeedback(int id, [FromBody] AssignmentFeedback af)
        {

            try
            {
                if (id != af.Id)
                {
                    return BadRequest();
                }
                return Ok(await service.Update(af));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentFeedbackController , Action: PutAssignmentFeedback , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/AssignmentFeedbackes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AssignmentFeedback>> PostAssignmentFeedback(AssignmentFeedback a)
        {
            try
            {
                //var isValid = service.GetAllAssignmentFeedbacks().AnyAsync(a => a.AssignmentAnswerId == dto.AssignmentAnswerId);
                //if(!isValid)
                //    return BadRequest("invalid AssignmentAnswerId");
                //var isValid = await service2.isValidAssignmentAnswerFk(dto.AssignmentAnswerId);
                //if (!isValid)
                //    return BadRequest("no such fk");
                //var af = new AssignmentFeedback { Feedback = dto.Feedback, AssignmentAnswerId = dto.AssignmentAnswerId}; 
                return Ok(await service.AddAsync(a));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentFeedbackController , Action: PostAssignmentFeedback , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/AssignmentFeedbackes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignmentFeedback(int id)
        {
            try
            {
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: AssignmentFeedbackController , Action: DeleteAssignmentFeedback , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
