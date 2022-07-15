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
    public class LessonsController : ControllerBase
    {
        private ILessonRepository service { get; }
        private readonly IMapper mapper;
        private readonly ICourseRepository courseRepository;
        public LessonsController(ILessonRepository _service, IMapper mapper, ICourseRepository courseRepository)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: LessonController , Action: GetLessons , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
            try
            {
                if (await service.GetByIdAsync(id) == null)
                    return NotFound($"Invalid lessonId : {id}");
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: LessonController , Action: GetLesson , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(int id, LessonDTO dto)
        {

            try
            {
                var isValidCourseId = await courseRepository.IsValidCourseId(dto.CourseId);
                if (!isValidCourseId) return BadRequest("Invalid CourseId");
                var lesson = await service.GetByIdAsync(id);
                if (lesson == null) return NotFound($"No Lesson With this id : {id}");
                lesson.Title = dto.Title;
                lesson.Description = dto.Description;
                lesson.CourseId = dto.CourseId;
                return Ok(await service.Update(lesson));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: LessonController , Action: PutLesson , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Lesson>> PostLesson(LessonDTO dto)
        {
            try
            {
                if (dto.Id != 0)
                    return BadRequest("Id is auto generated don't assign it.");
                var isValidCourseId = await courseRepository.IsValidCourseId(dto.CourseId);
                if (!isValidCourseId) return BadRequest("Invalid CourseId");
                var l = mapper.Map<Lesson>(dto);
                var added = await service.AddAsync(l);
                return Ok(added);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: LessonController , Action: PostLesson , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            try
            {
                var lesson = await service.GetByIdAsync(id);
                if (lesson == null) return NotFound($"No Lesson With this id : {id}");
                await service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: LessonController , Action: DeleteLesson , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("GetLessonsByCourseId/{courseId}")]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessonsByCourseId(int courseId)
        {
            try
            {
                var isValidCourseId = await courseRepository.IsValidCourseId(courseId);
                if (!isValidCourseId) return BadRequest("Invalid CourseId");

                var a = await service.GetLessonsByCourseId(courseId);
                //if (!a.Any()) return NotFound($"No Lessons were found with CourseId: {courseId}");
                return Ok(a);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: LessonController , Action: GetLessons , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        #region Old Services
        //            [HttpGet("GetByIdWithContent/{id}")]
        //            public async Task<ActionResult<IEnumerable<Lesson>>> GetByIdWithContent(int id)
        //            {
        //                try
        //                {
        //                    return Ok(await service.GetByIdWithContent(id));
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error($"Controller: CoursesController , Action: GetByIdWithContent , Message: {ex.Message}");
        //                    return BadRequest();
        //                }
        //                finally
        //                {
        //                    Log.CloseAndFlush();
        //                }
        //            }
        //            [HttpGet("GetByIdWithNotes/{id}")]
        //            public async Task<ActionResult<IEnumerable<Course>>> GetByIdWithNotes(int id)
        //            {
        //                try
        //                {
        //                    return Ok(await service.GetByIdWithNotes(id));
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error($"Controller: CoursesController , Action: GetByIdWithNotes , Message: {ex.Message}");
        //                    return BadRequest();
        //                }
        //                finally
        //                {
        //                    Log.CloseAndFlush();
        //                }
        //            }
        //            [HttpGet("GetByIdWithQuestions/{id}")]
        //            public async Task<ActionResult<IEnumerable<Course>>> GetByIdWithQuestions(int id)
        //            {
        //                try
        //                {
        //                    return Ok(await service.GetByIdWithQuestions(id));
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error($"Controller: CoursesController , Action: GetByIdWithQuestions , Message: {ex.Message}");
        //                    return BadRequest();
        //                }
        //                finally
        //                {
        //                    Log.CloseAndFlush();
        //                }

        //             }
        //    } 
        #endregion
    }
}
