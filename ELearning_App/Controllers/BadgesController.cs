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
    public class BadgesController : ControllerBase
    {
        private IBadgeRepository service { get; }

        public BadgesController(IBadgeRepository _service)
        {
            service = _service;
            new Logger();
        }

        // GET: api/Badgees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Badge>>> GetBadges()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: BadgeController , Action: GetBadges , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/Badgees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Badge>> GetBadge(int id)
        {
            try
            {
                if (service.GetByIdAsync(id) == null)
                    return NotFound();
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: BadgeController , Action: GetBadge , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/Badgees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBadge(int id, Badge b)
        {

            try
            {
                if (id != b.Id)
                {
                    return BadRequest();
                }
                return Ok(await service.Update(b));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: BadgeController , Action: PutBadge , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/Badgees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Badge>> PostBadge(Badge b)
        {
            try
            {
                return Ok(await service.AddAsync(b));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: BadgeController , Action: PostBadge , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/Badgees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBadge(int id)
        {
            try
            {
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: BadgeController , Action: DeleteBadge , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
