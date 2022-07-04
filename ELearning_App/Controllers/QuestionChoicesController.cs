using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositories;

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionChoicesController : ControllerBase
    {
        private IQuestionChoiceRepository service { get; }
        private readonly IQuestionRepository questionRepository;
        private readonly IMapper mapper;

        public QuestionChoicesController(IQuestionChoiceRepository _service, IMapper mapper, IQuestionRepository questionRepository)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.questionRepository = questionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionChoice>>> GetQuestionChoices()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionChoicesController , Action: GetQuestionChoices , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionChoice>> GetQuestionChoice(int id)
        {
            try
            {
                var questionChoice = await service.GetByIdAsync(id);
                if (questionChoice == null)
                    return NotFound($"Invalid questionChoice : {id}");
                return Ok(questionChoice);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionChoicesController , Action: GetQuestionChoice , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionChoice(int id, QuestionChoiceDTO dto)
        {

            try
            {
                var isValidQuestionId = await questionRepository.IsValidQuestionId(dto.QuestionId);
                var questionChoice = await service.GetByIdAsync(id);
                if (!isValidQuestionId)
                    return BadRequest($"Invalid questionId: {dto.QuestionId}");
                if (questionChoice == null)
                    return NotFound($"Invalid questionChoice : {id}");
                questionChoice.Choice = dto.Choice;
                //questionChoice.QuestionId = dto.QuestionId;
                return Ok(await service.Update(questionChoice));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionChoicesController , Action: PutQuestionChoice , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuestionChoice>> PostQuestionChoice(QuestionChoiceDTO dto)
        {
            try
            {
                if (dto.Id != 0)
                    return BadRequest("Id is auto generated don't assign it.");
                var isValidQuestionId = await questionRepository.IsValidQuestionId(dto.QuestionId);
                if (!isValidQuestionId)
                    return BadRequest($"Invalid questionId: {dto.QuestionId}");
                var mapped = mapper.Map<QuestionChoice>(dto);
                await service.AddAsync(mapped);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionChoicesController , Action: PostQuestionChoice , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionChoice(int id)
        {
            try
            {
                var questionChoice = await service.GetByIdAsync(id);
                if (questionChoice == null)
                    return NotFound($"Invalid questionChoice : {id}");
                await service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionChoicesController , Action: DeleteQuestionChoice , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpGet("GetQuestionChoicesByQuestionId/{questionId}")]
        public async Task<ActionResult<IEnumerable<QuestionChoice>>> GetQuestionChoicesByQuestionId(int questionId)
        {
            try
            {
                var isValidQuestionId = await questionRepository.IsValidQuestionId(questionId);
                var choices = await service.GetQuestionChoicesByQuestionId(questionId);
                if (!isValidQuestionId)
                    return BadRequest($"Invalid questionId : {questionId}");
                //if (!choices.Any())
                //    return NotFound($"There're No Choices with such questionId :{questionId}");
                return Ok(choices);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionChoicesController , Action: GetQuestionChoicesByQuestionId , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpPost("PostMultipleQuestionChoices")]
        public async Task<ActionResult> PostMultipleQuestionChoices(List<QuestionChoiceDTO> questionChoices)
        {
            try
            {
                for (int i = 0; i < questionChoices.Count(); i++)
                {
                    var isValidQuestionId = await questionRepository.IsValidQuestionId(questionChoices[i].QuestionId);
                    if (!isValidQuestionId)
                        questionChoices.Remove(questionChoices[i]);
                }
                var mapped = mapper.Map<List<QuestionChoice>>(questionChoices);
                await service.AddMultipleAsync(mapped);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuestionChoicesController , Action: PostMultipleQuestionChoices , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }


        }
    }
}
