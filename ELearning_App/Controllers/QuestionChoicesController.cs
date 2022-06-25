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

        // GET: api/Badgees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionChoice>>> GetQuestionChoices()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: BadgeController , Action: GetBadges , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/Badgees/5
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
                Log.Error($"Controller: BadgeController , Action: GetBadge , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/Badgees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
                Log.Error($"Controller: BadgeController , Action: PutBadge , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/Badgees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionChoice>> PostQuestionChoice(QuestionChoiceDTO dto)
        {
            try
            {
                var isValidQuestionId = await questionRepository.IsValidQuestionId(dto.QuestionId);
                if (!isValidQuestionId)
                    return BadRequest($"Invalid questionId: {dto.QuestionId}");
                var mapped = mapper.Map<QuestionChoice>(dto);
                return Ok(await service.AddAsync(mapped));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: BadgeController , Action: PostBadge , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/Badgees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionChoice(int id)
        {
            try
            {
                var questionChoice = await service.GetByIdAsync(id);
                if (questionChoice == null)
                    return NotFound($"Invalid questionChoice : {id}");
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: BadgeController , Action: DeleteBadge , Message: {ex.Message}");
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
                if (choices.Count() == 0)
                    return NotFound($"There're No Choices with such questionId :{questionId}");
                return Ok(choices);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: BadgeController , Action: GetQuestionChoicesByQuestionId , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
