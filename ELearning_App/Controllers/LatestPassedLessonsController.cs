//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ELearning_App.Domain.Entities;
//using ELearning_App.Helpers;
//using Serilog;

//namespace ELearning_App.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LatestPassedLessonsController : ControllerBase
//    {
//        private ILatestPassedLessonRepository service { get; }

//        public LatestPassedLessonsController(ILatestPassedLessonRepository _service)
//        {
//            service = _service;
//            new Logger();
//        }

//        // GET: api/LatestPassedLessones
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<LatestPassedLesson>>> GetLatestPassedLessons()
//        {
//            try
//            {
//                return Ok(await service.GetAllAsync());
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: LatestPassedLessonController , Action: GetLatestPassedLessons , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // GET: api/LatestPassedLessones/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<LatestPassedLesson>> GetLatestPassedLesson(int id)
//        {
//            try
//            {
//                if (service.GetByIdAsync(id) == null)
//                    return NotFound();
//                return Ok(await service.GetByIdAsync(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: LatestPassedLessonController , Action: GetLatestPassedLesson , Message: {ex.Message}");
//                return NotFound();
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // PUT: api/LatestPassedLessones/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutLatestPassedLesson(int id, LatestPassedLesson l)
//        {

//            try
//            {
//                if (id != l.Id)
//                {
//                    return BadRequest();
//                }
//                return Ok(await service.Update(l));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: LatestPassedLessonController , Action: PutLatestPassedLesson , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // POST: api/LatestPassedLessones
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<LatestPassedLesson>> PostLatestPassedLesson(LatestPassedLesson l)
//        {
//            try
//            {
//                return Ok(await service.AddAsync(l));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: LatestPassedLessonController , Action: PostLatestPassedLesson , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // DELETE: api/LatestPassedLessones/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteLatestPassedLesson(int id)
//        {
//            try
//            {
//                return Ok(await service.Delete(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: LatestPassedLessonController , Action: DeleteLatestPassedLesson , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }
//    }
//}
