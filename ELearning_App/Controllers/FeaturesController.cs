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
    public class FeaturesController : ControllerBase
    {
        private IFeatureRepository service { get; }
        private readonly IMapper mapper;
        private readonly IStudentRepository studentRepository;
        public FeaturesController(IFeatureRepository _service, IMapper mapper, IStudentRepository studentRepository)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.studentRepository = studentRepository;
        }

        // GET: api/Featurees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feature>>> GetFeatures()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: FeatureController , Action: GetFeatures , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/Featurees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feature>> GetFeature(int id)
        {
            try
            {
                if (await service.GetByIdAsync(id) == null)
                    return NotFound();
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: FeatureController , Action: GetFeature , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/Featurees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeature(int id, FeatureDTO f)
        {

            try
            {
                var isValidStudentId = await studentRepository.IsValidStudentId(f.StudentId);
                if (!isValidStudentId) return BadRequest("Invalid studentId");
                var feature = await service.GetByIdAsync(id);
                if (feature == null) return NotFound();
                feature.OldName = f.OldName;
                feature.NewName = f.NewName;
                feature.OldPath = f.OldPath;
                feature.NewPath = f.NewPath;
                feature.StudentId = f.StudentId;
                return Ok(await service.Update(feature));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: FeatureController , Action: PutFeature , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/Featurees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Feature>> PostFeature(FeatureDTO dto)
        {
            try
            {
                if (dto.Id != 0)
                    return BadRequest("Id is auto generated don't assign it.");
                var isValidStudentId = await studentRepository.IsValidStudentId(dto.StudentId);
                if (!isValidStudentId) return BadRequest("Invalid studentId");
                var feature = mapper.Map<Feature>(dto);
                await service.AddAsync(feature);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: FeatureController , Action: PostFeature , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/Featurees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            try
            {
                var feature = await service.GetByIdAsync(id);
                if (feature == null) return NotFound();
                await service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: FeatureController , Action: DeleteFeature , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
