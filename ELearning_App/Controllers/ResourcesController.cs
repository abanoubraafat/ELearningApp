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
    public class ResourcesController : ControllerBase
    {
        private readonly IMapper mapper;
        private IResourceRepository service { get; }

        public ResourcesController(IResourceRepository _service, IMapper _mapper)
        {
            service = _service;
            new Logger();
            mapper = _mapper;
        }

        // GET: api/Resourcees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceDTO>>> GetResources()
        {
            try
            {
                var r = await service.GetAllAsync();
                return Ok(r);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ResourceController , Action: GetResources , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/Resourcees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDTO>> GetResource(int id)
        {
            try
            {
                var r = await service.GetByIdAsync(id);
                if (r == null)
                    return NotFound($"No Resource was found with Id: {id}");
                return Ok(r);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ResourceController , Action: GetResource , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/Resourcees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResource(int id, ResourceDTO dto)
        {

            try
            {
                var resource = await service.GetByIdAsync(id);
                if (resource == null) return NotFound($"No Resource was found with Id: {id}");
                //var r = mapper.Map<Resource>(dto);
                resource.Title = dto.Title;
                resource.Type = dto.Type;
                resource.Image = dto.Image;
                resource.Path = dto.Path;
                //if (id != r.Id || r == null)
                //{
                //    return BadRequest("the resource isnot found");
                //}

                return Ok(await service.Update(resource));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ResourceController , Action: UpdateResource , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/Resourcees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Resource>> AddResource(ResourceDTO dto)
        {
            try
            {
                var r = mapper.Map<Resource>(dto);
                return Ok(await service.AddAsync(r));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ResourceController , Action: AddResource , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/Resourcees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            try
            {
                var resource = await service.GetByIdAsync(id);

                if (resource == null)
                    return NotFound($"No Resource was found with Id: {id}");
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ResourceController , Action: DeleteResource , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
