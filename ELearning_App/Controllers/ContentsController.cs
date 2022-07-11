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
                if (dto.PdfPath != null && !dto.PdfPath.Equals(content.PdfPath) || dto.VideoPath != null && !dto.VideoPath.Equals(content.VideoPath))
                {
                    return BadRequest("for updating the file use the specified endpoint for that");
                }
                content.ShowDate = dto.ShowDate;
                content.LessonId = dto.LessonId;
                content.Text = dto.Text;
                content.Link = dto.Link;
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
                var c = mapper.Map<Content>(dto);
                if (dto.PdfPath != null)
                {
                    if (!FilesConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.PdfPath.FileName).ToLower()))
                        return BadRequest("Only .pdf, .doc, .docx, .ppt, .pptx, .xlsx, .rar, .zip, .png, .jpg, .jpeg, .txt, .html, .htm files are allowed!");
                    var pdf = dto.PdfPath;
                    var randomName = Guid.NewGuid() + Path.GetExtension(dto.PdfPath.FileName);
                    var filePath = Path.Combine(_host.WebRootPath + "/Files", randomName);
                    using (FileStream fileStream = new(filePath, FileMode.Create))
                    {
                        await pdf.CopyToAsync(fileStream);
                    }
                    c.PdfPath = randomName;
                }
                if (dto.VideoPath != null)
                {
                    if (!VideosConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.VideoPath.FileName).ToLower()))
                        return BadRequest("Only .mp4, .mkv files are allowed!");
                    var vid = dto.VideoPath;
                    var randomName = Guid.NewGuid() + Path.GetExtension(dto.VideoPath.FileName);
                    var filePath = Path.Combine(_host.WebRootPath + "/Videos", randomName);
                    using (FileStream fileStream = new(filePath, FileMode.Create))
                    {
                        await vid.CopyToAsync(fileStream);
                    }
                    c.VideoPath = randomName;
                }

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
        public async Task<IActionResult> UpdateFile(int id, [FromForm] UpdateVidPdfContentDTO dto)
        {
            try
            {
                var content = await service.GetByIdAsync(id);
                if (content == null) return NotFound($"No Content was found with Id: {id}");
                if(dto.PdfPath != null || dto.VideoPath != null)
                {
                    if (dto.PdfPath != null)
                    {
                        if (!FilesConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.PdfPath.FileName).ToLower()))
                            return BadRequest("Only .pdf, .doc, .docx, .ppt, .pptx, .xlsx, .rar, .zip, .png, .jpg, .jpeg, .txt, .html, .htm files are allowed!");
                        var pdf = dto.PdfPath;
                        var randomName = Guid.NewGuid() + Path.GetExtension(dto.PdfPath.FileName);
                        var filePath = Path.Combine(_host.WebRootPath + "/Files", randomName);
                        using (FileStream fileStream = new(filePath, FileMode.Create))
                        {
                            await pdf.CopyToAsync(fileStream);
                        }
                        content.PdfPath = randomName;
                    }
                    if (dto.VideoPath != null)
                    {
                        if (!VideosConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.VideoPath.FileName).ToLower()))
                            return BadRequest("Only .mp4, .mkv files are allowed!");
                        var vid = dto.VideoPath;
                        var randomName = Guid.NewGuid() + Path.GetExtension(dto.VideoPath.FileName);
                        var filePath = Path.Combine(_host.WebRootPath + "/Videos", randomName);
                        using (FileStream fileStream = new(filePath, FileMode.Create))
                        {
                            await vid.CopyToAsync(fileStream);
                        }
                        content.PdfPath = randomName;
                    }
                    return Ok(await service.Update(content));
                }
                else
                {
                    return Ok(new { message = "No Files Updated" });
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
        [HttpPut("form/{id}")]
        public async Task<IActionResult> PutContentForm(int id,[FromForm] ContentDTO dto)
        {

            try
            {
                var isValidLessonId = await lessonRepository.IsValidLessonId(dto.LessonId);
                if (!isValidLessonId)
                    return BadRequest("Invalid LessonId");
                var content = await service.GetByIdAsync(id);
                if (content == null) return NotFound();
                if (dto.PdfPath != null)
                {
                    if (!FilesConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.PdfPath.FileName).ToLower()))
                        return BadRequest("Only .pdf, .doc, .docx, .ppt, .pptx, .xlsx, .rar, .zip, .png, .jpg, .jpeg, .txt, .html, .htm files are allowed!");
                    var pdf = dto.PdfPath;
                    var randomName = Guid.NewGuid() + Path.GetExtension(dto.PdfPath.FileName);
                    var filePath = Path.Combine(_host.WebRootPath + "/Files", randomName);
                    using (FileStream fileStream = new(filePath, FileMode.Create))
                    {
                        await pdf.CopyToAsync(fileStream);
                    }
                    content.PdfPath = randomName;
                }
                if (dto.VideoPath != null)
                {
                    if (!VideosConstraints.allowedExtenstions.Contains(Path.GetExtension(dto.VideoPath.FileName).ToLower()))
                        return BadRequest("Only .mp4, .mkv files are allowed!");
                    var vid = dto.VideoPath;
                    var randomName = Guid.NewGuid() + Path.GetExtension(dto.VideoPath.FileName);
                    var filePath = Path.Combine(_host.WebRootPath + "/Videos", randomName);
                    using (FileStream fileStream = new(filePath, FileMode.Create))
                    {
                        await vid.CopyToAsync(fileStream);
                    }
                    content.VideoPath = randomName;
                }
                content.ShowDate = dto.ShowDate;
                content.LessonId = dto.LessonId;
                content.Text = dto.Text;
                content.Link = dto.Link;
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
    }
}
