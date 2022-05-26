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
//    public class QuizAnswersController : ControllerBase
//    {
//        private IQuizAnswerRepository service { get; }

//        public QuizAnswersController(IQuizAnswerRepository _service)
//        {
//            service = _service;
//            new Logger();
//        }

//        // GET: api/QuizAnsweres
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<QuizAnswer>>> GetQuizAnswers()
//        {
//            try
//            {
//                return Ok(await service.GetAllAsync());
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizAnswerController , Action: GetQuizAnswers , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // GET: api/QuizAnsweres/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<QuizAnswer>> GetQuizAnswer(int id)
//        {
//            try
//            {
//                if (service.GetByIdAsync(id) == null)
//                    return NotFound();
//                return Ok(await service.GetByIdAsync(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizAnswerController , Action: GetQuizAnswer , Message: {ex.Message}");
//                return NotFound();
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // PUT: api/QuizAnsweres/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutQuizAnswer(int id, QuizAnswer q)
//        {

//            try
//            {
//                if (id != q.Id)
//                {
//                    return BadRequest();
//                }
//                return Ok(await service.Update(q));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizAnswerController , Action: PutQuizAnswer , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // POST: api/QuizAnsweres
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<QuizAnswer>> PostQuizAnswer(QuizAnswer q)
//        {
//            try
//            {
//                return Ok(await service.AddAsync(q));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizAnswerController , Action: PostQuizAnswer , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // DELETE: api/QuizAnsweres/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteQuizAnswer(int id)
//        {
//            try
//            {
//                return Ok(await service.Delete(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: QuizAnswerController , Action: DeleteQuizAnswer , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }
//        //[HttpGet("GetByIdWithGrade/{id}")]
//        //public async Task<ActionResult<QuizAnswer>> GetByIdWithGrade(int id)
//        //{
//        //    try
//        //    {
//        //        return Ok(await service.GetByIdWithGrade(id));
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Log.Error($"Controller: QuizAnswerController , Action: GetByIdWithGrade , Message: {ex.Message}");
//        //        return StatusCode(500);
//        //    }
//        //    finally
//        //    {
//        //        Log.CloseAndFlush();
//        //    }
//        //}
//    }
//}
