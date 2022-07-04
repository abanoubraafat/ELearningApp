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
        private readonly IWebHostEnvironment _host;
        public ContentsController(IContentRepository _service, IMapper mapper, ILessonRepository lessonRepository, IWebHostEnvironment host)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.lessonRepository = lessonRepository;
            _host = host;
        }

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Content>> GetContent(int id)
        {
            try
            {
                if (await service.GetByIdAsync(id) == null)
                    return NotFound($"Invalid contentId : {id}");
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContent(int id, UpdateContentDTO dto)
        {

            try
            {
                var isValidLessonId = await lessonRepository.IsValidLessonId(dto.LessonId);
                if (!isValidLessonId)
                    return BadRequest("Invalid LessonId");
                var content = await service.GetByIdAsync(id);
                if (content == null) return NotFound();
                content.FileName = dto.FileName;
                if (dto.Path != null && !dto.Path.Equals(content.Path))
                {
                    return BadRequest("for updating the file use the specified endpoint for that");
                }
                content.ShowDate = dto.ShowDate;
                content.LessonId = dto.LessonId;
                return Ok(await service.Update(content));
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

        [HttpPost]
        public async Task<ActionResult<Content>> PostContent([FromForm] ContentDTO dto)
        {
            try
            {
                if (dto.Id != 0)
                    return BadRequest("Id is auto generated don't assign it.");
                var isValidLessonId = await lessonRepository.IsValidLessonId(dto.LessonId);
                if(!isValidLessonId)
                    return BadRequest("Invalid LessonId");
                if (!ContentConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.Path.FileName).ToLower()))
                    return BadRequest("Only .pdf, .doc, .docx, .ppt, .pptx, .xlsx, .rar, .zip, .png, .jpg, .jpeg, .txt, .mp4, .mp3 and .mkv files are allowed!");
                var c = mapper.Map<Content>(dto);
                var img = dto.Path;
                var randomName = Guid.NewGuid() + Path.GetExtension(dto.Path.FileName);
                var filePath = Path.Combine(_host.WebRootPath + "/Content", randomName);
                using (FileStream fileStream = new(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }
                c.Path = @"\\Abanoub\wwwroot\Content\" + randomName;
                await service.AddAsync(c);
                return Ok();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(int id)
        {
            try
            {
                var content = await service.GetByIdAsync(id);
                if (content == null) return NotFound();
                await service.Delete(id);
                return Ok();
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
        [HttpGet("GetContentsByLessonId/{lessonId}")]
        public async Task<ActionResult<IEnumerable<Content>>> GetContentsByLessonId(int lessonId)
        {
            try
            {
                var isValidLessonId = await lessonRepository.IsValidLessonId(lessonId);
                if (!isValidLessonId)
                    return BadRequest("Invalid LessonId");
                var content = await service.GetContentsByLessonId(lessonId);
                //if (!content.Any())
                //    return NotFound($"No Content was found with lessonId : {lessonId}");
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
        [HttpPut("update-file/{id}")]
        public async Task<IActionResult> UpdateFile(int id, [FromForm] UpdateFileDTO dto)
        {
            try
            {
                var content = await service.GetByIdAsync(id);
                if (content == null) return NotFound($"No Content was found with Id: {id}");
                if (dto.File != null)
                {
                    if (!ContentConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.File.FileName).ToLower()))
                        return BadRequest("Only .pdf, .doc, .docx, .ppt, .pptx, .xlsx, .rar, .zip, .png, .jpg, .jpeg, .txt, .mp4, .mp3 and .mkv files are allowed!");
                    var img = dto.File;
                    var randomName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
                    var filePath = Path.Combine(_host.WebRootPath + "/Content", randomName);
                    using (FileStream fileStream = new(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(fileStream);
                    }
                    content.Path = @"\\Abanoub\wwwroot\Content\" + randomName;
                    return Ok(await service.Update(content));
                }
                else
                {
                    return BadRequest("file can't be null;");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: ContentsController , Action: UpdateFile , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
