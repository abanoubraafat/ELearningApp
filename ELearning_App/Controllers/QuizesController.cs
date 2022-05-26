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
//    public class QuizesController : ControllerBase
//    {
//        private IQuizRepository service { get; }

//        public QuizesController(IQuizRepository _service)
//        {
//            service = _service;
//            new Logger();
//        }

//        // GET: api/Quizes
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizs()
//        {
//            try
//            {
//                return Ok(await service.GetAllAsync());
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizController , Action: GetQuizs , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // GET: api/Quizes/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Quiz>> GetQuiz(int id)
//        {
//            try
//            {
//                if (service.GetByIdAsync(id) == null)
//                    return NotFound();
//                return Ok(await service.GetByIdAsync(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizController , Action: GetQuiz , Message: {ex.Message}");
//                return NotFound();
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // PUT: api/Quizes/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutQuiz(int id, Quiz q)
//        {

//            try
//            {
//                if (id != q.Id)
//                {
//                    return BadRequest();
//                }
//                return Ok(await service.UpdateQuiz(q));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizController , Action: PutQuiz , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // POST: api/Quizes
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Quiz>> PostQuiz(Quiz q)
//        {
//            try
//            {
//                return Ok(await service.AddQuiz(q));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizController , Action: PostQuiz , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // DELETE: api/Quizes/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteQuiz(int id)
//        {
//            try
//            {
//                return Ok(await service.DeleteQuiz(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizController , Action: DeleteQuiz , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }
//        [HttpGet("GetByIdWithAnswers/{id}")]
//        public async Task<ActionResult<Quiz>> GetByIdWithAnswers(int id)
//        {
//            try
//            {
//                return Ok(await service.GetByIdWithAnswers(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizController , Action: GetByIdWithAnswers , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }
//    }
//}
