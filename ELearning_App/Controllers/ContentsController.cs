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
    public class ContentsController : ControllerBase
    {
        private IContentRepository service { get; }
        private readonly IMapper mapper;
        private readonly ILessonRepository lessonRepository;

        public ContentsController(IContentRepository _service, IMapper mapper, ILessonRepository lessonRepository)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.lessonRepository = lessonRepository;
        }

        // GET: api/Contentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Content>>> GetContents()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ContentController , Action: GetContents , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/Contentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Content>> GetContent(int id)
        {
            try
            {
                if (await service.GetByIdAsync(id) == null)
                    return NotFound();
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ContentController , Action: GetContent , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/Contentes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContent(int id, Content c)
        {

            try
            {
                var isValidLessonId = await lessonRepository.IsValidLessonId(c.LessonId);
                if (!isValidLessonId)
                    return BadRequest("Invalid LessonId");
                var content = await service.GetByIdAsync(id);
                if (content == null) return NotFound();
                return Ok(await service.Update(c));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ContentController , Action: PutContent , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/Contentes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Content>> PostContent(Content c)
        {
            try
            {
                var isValidLessonId = await lessonRepository.IsValidLessonId(c.LessonId);
                if(!isValidLessonId)
                    return BadRequest("Invalid LessonId");
                return Ok(await service.AddAsync(c));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ContentController , Action: PostContent , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/Contentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(int id)
        {
            try
            {
                var content = await service.GetByIdAsync(id);
                if (content == null) return NotFound();
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ContentController , Action: DeleteContent , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("Lesson/{lessonId}/Content")]
        public async Task<ActionResult<IEnumerable<Content>>> GetContentsByLessonId(int lessonId)
        {
            try
            {
                var isValidLessonId = await lessonRepository.IsValidLessonId(lessonId);
                if (!isValidLessonId)
                    return BadRequest("Invalid LessonId");
                var content = await service.GetContentsByLessonId(lessonId);
                if (content.Count() == 0)
                    return NotFound($"No Content was found with lessonId : {lessonId}");
                return Ok(content);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ContentController , Action: GetContentsByLessonId , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
