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
//    public class QuestionsController : ControllerBase
//    {
//        private IQuestionRepository service { get; }

//        public QuestionsController(IQuestionRepository _service)
//        {
//            service = _service;
//            new Logger();
//        }

//        // GET: api/Questiones
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
//        {
//            try
//            {
//                return Ok(await service.GetAllQuestions());
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuestionController , Action: GetQuestions , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // GET: api/Questiones/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Question>> GetQuestion(int id)
//        {
//            try
//            {
//                if (service.GetByIdAsync(id) == null)
//                    return NotFound();
//                return Ok(await service.GetByIdAsync(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuestionController , Action: GetQuestion , Message: {ex.Message}");
//                return NotFound();
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // PUT: api/Questiones/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutQuestion(int id, Question q)
//        {

//            try
//            {
//                if (id != q.Id)
//                {
//                    return BadRequest();
//                }
//                return Ok(await service.UpdateQuestion(q));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuestionController , Action: PutQuestion , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // POST: api/Questiones
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Question>> PostQuestion(Question q)
//        {
//            try
//            {
//                return Ok(await service.AddQuestion(q));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuestionController , Action: PostQuestion , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // DELETE: api/Questiones/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteQuestion(int id)
//        {
//            try
//            {
//                return Ok(await service.DeleteQuestion(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuestionController , Action: DeleteQuestion , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }
//        [HttpGet("GetByIdWithAnswers/{id}")]
//        public async Task<ActionResult<Question>> GetByIdWithAnswers(int id)
//        {
//            try
//            {
//                return Ok(await service.GetByIdWithAnswers(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuestionController , Action: GetByIdWithAnswers , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }
//    }
//}
