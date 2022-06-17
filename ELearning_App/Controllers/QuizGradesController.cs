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
    public class QuizGradesController : ControllerBase
    {
        private IQuizGradeRepository service { get; }
        private readonly IQuizRepository quizRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        public QuizGradesController(IQuizGradeRepository _service, IMapper mapper, IQuizRepository quizRepository, IStudentRepository studentRepository)
        {
            service = _service;
            new Logger();
            this.mapper = mapper;
            this.quizRepository = quizRepository;
            this.studentRepository = studentRepository;
        }

        // GET: api/QuizGradees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizGrade>>> GetQuizGrades()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizGradeController , Action: GetQuizGrades , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // GET: api/QuizGradees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizGrade>> GetQuizGrade(int id)
        {
            try
            {
                if (service.GetByIdAsync(id) == null)
                    return NotFound();
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizGradeController , Action: GetQuizGrade , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // PUT: api/QuizGradees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizGrade(int id, QuizGradeDTO dto)
        {

            try
            {
                var isValidStudentId = await studentRepository.IsValidStudentId(dto.StudentId);
                var isValidQuizId = await quizRepository.IsValidQuizId(dto.QuizId);
                var quizGrade = await service.GetByIdAsync(id);
                if (!isValidStudentId)
                    return BadRequest($"Invalid studentId : {dto.StudentId}");
                if (!isValidQuizId)
                    return BadRequest($"Invalid quizId : {dto.QuizId}");
                if (quizGrade == null)
                    return NotFound($"Invalid quizGradeId : {id}");
                quizGrade.Grade = dto.Grade;
                //quizGrade.QuizId = dto.QuizId;
                //quizGrade.StudentId = dto.StudentId;
                return Ok(await service.Update(quizGrade));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizGradeController , Action: PutQuizGrade , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // POST: api/QuizGradees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuizGrade>> PostQuizGrade(QuizGradeDTO dto)
        {
            try
            {
                var isValidStudentId = await studentRepository.IsValidStudentId(dto.StudentId);
                var isValidQuizId = await quizRepository.IsValidQuizId(dto.QuizId);
                var isNotValidQuizGrade = await service.IsNotValidQuizGrade(dto.StudentId, dto.QuizId);
                if (!isValidStudentId)
                    return BadRequest($"Invalid studentId : {dto.StudentId}");
                if (!isValidQuizId)
                    return BadRequest($"Invalid quizId : {dto.QuizId}");
                if (isNotValidQuizGrade)
                    return BadRequest($"There's already a QuizGrade to student {dto.StudentId} of quiz {dto.QuizId}");
                int grade = await service.QuizGradeAdderInt(dto.StudentId, dto.QuizId);
                if(dto.Grade == 0) //automatically set the grade if value entered by the user = 0, manyally otherwise.
                    dto.Grade = grade;
                var mapped = mapper.Map<QuizGrade>(dto);
                return Ok(await service.AddAsync(mapped));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizGradeController , Action: PostQuizGrade , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // DELETE: api/QuizGradees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuizGrade(int id)
        {
            try
            {
                var quizGrade = await service.GetByIdAsync(id);
                if (quizGrade == null)
                    return NotFound($"Invalid quizGradeId : {id}");
                return Ok(await service.Delete(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizGradeController , Action: DeleteQuizGrade , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("GetQuizGradesByQuizId/{quizId}")]
        public async Task<ActionResult<IEnumerable<QuizGradeDetailsDTO>>> GetQuizGradesByQuizId(int quizId)
        {
            //try
            //{
                var isValidQuizId = await quizRepository.IsValidQuizId(quizId);
                var quizGrades = await service.GetQuizGradesByQuizId(quizId);
                if (!isValidQuizId)
                    return BadRequest($"Invalid quizId :{quizId}");
                if (quizGrades.Count() == 0)
                    return NotFound($"There're No QuizGrades with such quizId: {quizId}");
                return Ok(mapper.Map<IEnumerable<QuizGradeDetailsDTO>>(quizGrades));
            //}
            //catch (Exception ex)
            //{
            //    Log.Error($"Controller: QuizGradeController , Action: GetQuizGradesByQuizId , Message: {ex.Message}");
            //    return StatusCode(500);
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
        }
        [HttpGet("GetQuizGradeByQuizIdByStudentId/{studentId}/{quizId}")]
        public async Task<ActionResult<QuizGrade>> GetQuizGradeByQuizIdByStudentId(int quizId, int studentId)
        {
            try
            {
                var isValidQuizId = await quizRepository.IsValidQuizId(quizId);
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                var quizGrade = await service.GetQuizGradeByQuizIdByStudentId(quizId, studentId);
                if (!isValidQuizId)
                    return BadRequest($"Invalid quizId :{quizId}");
                if(!isValidStudentId)
                    return BadRequest($"Invalid studentId :{studentId}");
                if (quizGrade == null)
                    return NotFound($"There're No QuizGrades with such quizId: {quizId}");
                return Ok(quizGrade);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizGradeController , Action: GetQuizGradeByQuizIdByStudentId , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("QuizGradeAdder/{studentId}/{quizId}")]
        public async Task<ActionResult<QuizGrade>> QuizGrader(int studentId, int quizId)
        {
            try
            {
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                var isValidQuizId = await quizRepository.IsValidQuizId(quizId);
                var grade = await service.QuizGradeAdder(studentId, quizId);
                if (!isValidQuizId)
                    return BadRequest($"Invalid quizId :{quizId}");
                if (!isValidStudentId)
                    return BadRequest($"Invalid studentId :{studentId}");
                if (grade == null)
                    return NotFound($"Post A grade to this quiz first");
                return Ok(grade);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizGradeController , Action: GetQuizGradeByQuizIdByStudentId , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
