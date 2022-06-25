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
using Repository.IRepositories;

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private IQuestionRepository service { get; }
        private readonly IQuizRepository quizRepository;
        private readonly IMapper mapper;
        private readonly IQuestionChoiceRepository questionChoiceRepository;
        public QuestionsController(IQuestionRepository _service, IQuizRepository quizRepository, IMapper mapper, IQuestionChoiceRepository questionChoiceRepository)
        {
            service = _service;
            this.quizRepository = quizRepository;
            new Logger();
            this.mapper = mapper;
            this.questionChoiceRepository = questionChoiceRepository;
        }

        // GET: api/Questiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionController , Action: GetQuestions , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/Questiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            try
            {
                var question = await service.GetByIdAsync(id);
                if (question == null)
                    return NotFound($"Invalid questionId {id}");
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionController , Action: GetQuestion , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/Questiones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, QuestionDTO dto)
        {

            try
            {
                var isValidQuizId = await quizRepository.IsValidQuizId(dto.QuizId);
                var question = await service.GetByIdAsync(id);
                if (!isValidQuizId)
                    return BadRequest($"Invalid quizId: {dto.QuizId}");
                if (question == null)
                    return NotFound($"Invalid questionId: {id}");
                question.Title = dto.Title;
                question.correctAnswer = dto.correctAnswer;
                question.ShowDate = dto.ShowDate;
                //question.QuizId = dto.QuizId;
                return Ok(await service.Update(question));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionController , Action: PutQuestion , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/Questiones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(QuestionDTO dto)
        {
            try
            {
                var isValidQuizId = await quizRepository.IsValidQuizId(dto.QuizId);
                if (!isValidQuizId)
                    return BadRequest($"Invalid quizId: {dto.QuizId}");
                var question = mapper.Map<Question>(dto);
                return Ok(await service.AddAsync(question));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionController , Action: PostQuestion , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/Questiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var question = await service.GetByIdAsync(id);
                if (question == null)
                    return NotFound($"Invalid questionId: {id}");
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionController , Action: DeleteQuestion , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("GetQuestionsByQuizId/{quizId}")]
        public async Task<ActionResult<IEnumerable<QuestionDetailsDTO>>> GetQuestionsByQuizId(int quizId)
        {
            try
            {
                var isValidQuizId = await quizRepository.IsValidQuizId(quizId);
                if (!isValidQuizId) return BadRequest($"Invalid QuizId : {quizId}");
                var questions = await service.GetQuestionsByQuizId(quizId);
                if (questions.Count() == 0)
                    return NotFound();
                var questionsWithChoices = mapper.Map<IEnumerable<QuestionDetailsDTO>>(questions);
                foreach(var questionWithChoice in questionsWithChoices)
                {
                    questionWithChoice.QuestionChoices = await questionChoiceRepository.GetQuestionChoicesByQuestionId(questionWithChoice.Id);
                }
                
                return Ok(questionsWithChoices);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionController , Action: GetQuestionsByQuizId , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        //[HttpGet("GetByIdWithAnswers/{id}")]
        //public async Task<ActionResult<Question>> GetByIdWithAnswers(int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithAnswers(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: QuestionController , Action: GetByIdWithAnswers , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
    }
}
