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
//    public class QuestionAnswersController : ControllerBase
//    {
//        private IQuestionAnswerRepository service { get; }

//        public QuestionAnswersController(IQuestionAnswerRepository _service)
//        {
//            service = _service;
//            new Logger();
//        }

//        // GET: api/QuestionAnsweres
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<QuestionAnswer>>> GetQuestionAnswers()
//        {
//            try
//            {
//                return Ok(await service.GetAllQuestionAnswers());
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuestionAnswerController , Action: GetQuestionAnswers , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // GET: api/QuestionAnsweres/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<QuestionAnswer>> GetQuestionAnswer(int id)
//        {
//            try
//            {
//                if (service.GetByIdAsync(id) == null)
//                    return NotFound();
//                return Ok(await service.GetByIdAsync(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuestionAnswerController , Action: GetQuestionAnswer , Message: {ex.Message}");
//                return NotFound();
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // PUT: api/QuestionAnsweres/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutQuestionAnswer(int id, QuestionAnswer qa)
//        {

//            try
//            {
//                if (id != qa.Id)
//                {
//                    return BadRequest();
//                }
//                return Ok(await service.UpdateQuestionAnswer(qa));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuestionAnswerController , Action: PutQuestionAnswer , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // POST: api/QuestionAnsweres
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<QuestionAnswer>> PostQuestionAnswer(QuestionAnswer qa)
//        {
//            try
//            {
//                return Ok(await service.AddQuestionAnswer(qa));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuestionAnswerController , Action: PostQuestionAnswer , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // DELETE: api/QuestionAnsweres/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteQuestionAnswer(int id)
//        {
//            try
//            {
//                return Ok(await service.DeleteQuestionAnswer(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuestionAnswerController , Action: DeleteQuestionAnswer , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }
//    }
//}
