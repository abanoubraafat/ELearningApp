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
    public class QuestionAnswersController : ControllerBase
    {
        private IQuestionAnswerRepository service { get; }
        private readonly IStudentRepository studentRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IMapper mapper;
        public QuestionAnswersController(IQuestionAnswerRepository _service, IMapper mapper, IStudentRepository studentRepository, IQuestionRepository questionRepository)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.studentRepository = studentRepository;
            this.questionRepository = questionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionAnswer>>> GetQuestionAnswers()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionAnswerController , Action: GetQuestionAnswers , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionAnswer>> GetQuestionAnswer(int id)
        {
            try
            {
                var questionAnswer = await service.GetByIdAsync(id);
                if (questionAnswer == null)
                    return NotFound($"Invalid questionAnswerId : {id}");
                return Ok(questionAnswer);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionAnswerController , Action: GetQuestionAnswer , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionAnswer(int id, QuestionAnswerDTO dto)
        {

            try
            {
                var isValidStudentId = await studentRepository.IsValidStudentId(dto.StudentId);
                var isValidQuestionId = await questionRepository.IsValidQuestionId(dto.QuestionId);
                var questionAnswer = await service.GetByIdAsync(id);
                if (!isValidStudentId)
                    return BadRequest($"Invalid studentId : {dto.StudentId}");
                if (!isValidQuestionId)
                    return BadRequest($"Invalid questionId : {dto.QuestionId}");
                if (questionAnswer == null)
                    return NotFound($"Invalid questionAnswerId : {id}");
                questionAnswer.Answer = dto.Answer;
                //questionAnswer.QuestionId = dto.QuestionId;
                //questionAnswer.StudentId = dto.StudentId;
                return Ok(await service.Update(questionAnswer));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionAnswerController , Action: PutQuestionAnswer , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuestionAnswer>> PostQuestionAnswer(QuestionAnswerDTO dto)
        {
            try
            {
                if (dto.Id != 0)
                    return BadRequest("Id is auto generated don't assign it.");
                var isValidStudentId = await studentRepository.IsValidStudentId(dto.StudentId);
                var isValidQuestionId = await questionRepository.IsValidQuestionId(dto.QuestionId);
                var isNotValidQuestionAnswer = await service.IsNotValidQuestionAnswer(dto.StudentId, dto.QuestionId);
                if (!isValidStudentId)
                    return BadRequest($"Invalid studentId : {dto.StudentId}");
                if (!isValidQuestionId)
                    return BadRequest($"Invalid questionId : {dto.QuestionId}");
                if (isNotValidQuestionAnswer)
                    return BadRequest($"There's already a QuestionAnswer assigned by student :{dto.StudentId} to question {dto.QuestionId}");
                var mapped = mapper.Map<QuestionAnswer>(dto);
                await service.AddAsync(mapped);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionAnswerController , Action: PostQuestionAnswer , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionAnswer(int id)
        {
            try
            {
                var questionAnswer = await service.GetByIdAsync(id);
                if (questionAnswer == null)
                    return NotFound($"Invalid questionAnswerId : {id}");
                await service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionAnswerController , Action: DeleteQuestionAnswer , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("CorrectQuestionAnswerOrNot/{questionId}/{questionAnswerId}")]
        public async Task<ActionResult<QuestionAnswer>> CorrectQuestionAnswerOrNot(int questionId, int questionAnswerId)
        {
            try
            {
                var isValidQuestionId = await questionRepository.IsValidQuestionId(questionId);
                var isValidQuestionAnswerId = await service.IsValidQuestionAnswerId(questionAnswerId);
                if (!isValidQuestionId)
                    return BadRequest($"Invalid questionId : {questionId}");
                if(!isValidQuestionAnswerId)
                    return NotFound($"Invalid questionAnswerId : {questionId}");
                return Ok(await service.CorrectQuestionAnswerOrNot(questionId, questionAnswerId));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionAnswerController , Action: CorrectQuestionAnswerOrNot , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("GetQuestionAnswersByQuestionId/{questionId}")]
        public async Task<ActionResult<IEnumerable<QuestionAnswer>>> GetQuestionAnswersByQuestionId(int questionId)
        {
            try
            {
                var isValidQuestionId = await questionRepository.IsValidQuestionId(questionId);
                var answers = await service.GetQuestionAnswersByQuestionId(questionId);
                if (!isValidQuestionId)
                    return BadRequest($"Invalid questionId : {questionId}");
                //if (!answers.Any())
                //    return NotFound($"There're No QuestionAnswers with such questionId : {questionId}");
                return Ok(answers);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionAnswerController , Action: GetQuestionAnswersByQuestionId , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        
        [HttpGet("GetQuestionAnswerByQuestionIdByStudentId/{studentId}/{questionId}")]
        public async Task<ActionResult<QuestionAnswer>> GetQuestionAnswerByQuestionIdByStudentId(int questionId, int studentId)
        {
            try
            {
                var isValidQuestionId = await questionRepository.IsValidQuestionId(questionId);
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                var answer = await service.GetQuestionAnswerByQuestionIdByStudentId(questionId, studentId);
                if (!isValidQuestionId)
                    return BadRequest($"Invalid questionId : {questionId}");
                if(!isValidStudentId)
                    return BadRequest($"Invalid studentId : {studentId}");
                if (answer == null)
                    return NotFound($"There're No QuestionAnswer with such questionId : {questionId}");
                return Ok(answer);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionAnswerController , Action: GetQuestionAnswersByQuestionId , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        
        [HttpPost("PostMultipleQuestionAnswers")]
        public async Task<ActionResult> PostMultipleQuestionAnswers(List<QuestionAnswerDTO> questionAnswers)
        {
            try
            {
                for (int i = 0; i < questionAnswers.Count; i++)
                {
                    var isValidStudentId = await studentRepository.IsValidStudentId(questionAnswers[i].StudentId);
                    var isValidQuestionId = await questionRepository.IsValidQuestionId(questionAnswers[i].QuestionId);
                    var isNotValidQuestionAnswer = await service.IsNotValidQuestionAnswer(questionAnswers[i].StudentId, questionAnswers[i].QuestionId);
                    if (!isValidStudentId || !isValidQuestionId || isNotValidQuestionAnswer)
                    {
                        questionAnswers.Remove(questionAnswers[i]);
                    }
                }
                var mapped = mapper.Map<List<QuestionAnswer>>(questionAnswers);

                await service.AddMultipleAsync(mapped);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionAnswersController , Action: PostMultipleQuestionAnswers , Message: {ex.Message}");
                return BadRequest();
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}
