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
//    public class QuizGradesController : ControllerBase
//    {
//        private IQuizGradeRepository service { get; }

//        public QuizGradesController(IQuizGradeRepository _service)
//        {
//            service = _service;
//            new Logger();
//        }

//        // GET: api/QuizGradees
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<QuizGrade>>> GetQuizGrades()
//        {
//            try
//            {
//                return Ok(await service.GetAllAsync());
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizGradeController , Action: GetQuizGrades , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // GET: api/QuizGradees/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<QuizGrade>> GetQuizGrade(int id)
//        {
//            try
//            {
//                if (service.GetByIdAsync(id) == null)
//                    return NotFound();
//                return Ok(await service.GetByIdAsync(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizGradeController , Action: GetQuizGrade , Message: {ex.Message}");
//                return NotFound();
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // PUT: api/QuizGradees/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutQuizGrade(int id, QuizGrade qg)
//        {

//            try
//            {
//                if (id != qg.Id)
//                {
//                    return BadRequest();
//                }
//                return Ok(await service.UpdateQuizGrade(qg));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizGradeController , Action: PutQuizGrade , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // POST: api/QuizGradees
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<QuizGrade>> PostQuizGrade(QuizGrade qg)
//        {
//            try
//            {
//                return Ok(await service.AddQuizGrade(qg));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizGradeController , Action: PostQuizGrade , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // DELETE: api/QuizGradees/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteQuizGrade(int id)
//        {
//            try
//            {
//                return Ok(await service.DeleteQuizGrade(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizGradeController , Action: DeleteQuizGrade , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }
//    }
//}
