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
    public class QuizesController : ControllerBase
    {
        private IQuizRepository service { get; }
        private readonly ICourseRepository courseRepository;
        public readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        public QuizesController(IQuizRepository _service, IMapper mapper, ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            service = _service;
            this.mapper = mapper;
            new Logger();
            this.courseRepository = courseRepository;
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetQuizDTO>>> GetQuizzes()
        {
            try
            {
                var quizzes = await service.GetAllAsync();
                var mapped = mapper.Map<IEnumerable<GetQuizDTO>>(quizzes);
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizController , Action: GetQuizs , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetQuizDTO>> GetQuiz(int id)
        {
            try
            {
                var quiz = await service.GetByIdAsync(id);
                if (quiz == null)
                    return NotFound($"Invalid quizId : {id}");
                var mapped = mapper.Map<GetQuizDTO>(quiz);
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizController , Action: GetQuiz , Message: {ex.Message}");
                return NotFound();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, QuizDTO q)
        {

            try
            {
                var isValidCourseId = await courseRepository.IsValidCourseId(q.CourseId);
                if (!isValidCourseId)
                    return BadRequest($"Invalid courseId: {q.CourseId}");
                var quiz = await service.GetByIdAsync(id);
                if (quiz == null)
                    return NotFound($"Invalid quizId: {id}");
                quiz.Title = q.Title;
                quiz.Instructions = q.Instructions;
                quiz.TotalPoints = q.TotalPoints;
                quiz.StartTime = q.StartTime;
                quiz.EndTime = q.EndTime;
                quiz.PostTime = q.PostTime;
                //quiz.CourseId = q.CourseId;
                var updatedQuiz = await service.Update(quiz);
                var mapped = mapper.Map<GetQuizDTO>(updatedQuiz);
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizController , Action: PutQuiz , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(QuizDTO q)
        {
            try
            {
                if (q.Id != 0)
                    return BadRequest("Id is auto generated don't assign it.");
                var isValidCourseId = await courseRepository.IsValidCourseId(q.CourseId);
                if (!isValidCourseId)
                    return BadRequest($"Invalid courseId: {q.CourseId}");
                var quiz = mapper.Map<Quiz>(q);
                await service.AddAsync(quiz);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizController , Action: PostQuiz , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            try
            {
                var quiz = await service.GetByIdAsync(id);
                if (quiz == null)
                    return NotFound($"Invalid quizId : {id}");
                await service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizController , Action: DeleteQuiz , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("GetQuizzesByCourseId/{courseId}")]
        public async Task<ActionResult<IEnumerable<GetQuizDTO>>> GetQuizzesByCourseId(int courseId)
        {
            try
            {
                var isValidCourseId = await courseRepository.IsValidCourseId(courseId);
                var quizzes = await service.GetQuizzesByCourseId(courseId);
                if (!isValidCourseId)
                    return BadRequest($"Invalid courseId: {courseId}");
                //if (!quizzes.Any())
                //    return NotFound($"There're No Quizzes with such courseId ; {courseId}");
                var mapped = mapper.Map<IEnumerable<GetQuizDTO>>(quizzes);
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizController , Action: GetQuizzesByCourseId , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [HttpGet("GetQuizGrades/ByCourseId/ByStudentId/ForTeacher/{courseId}/{studentId}")]
        public async Task<ActionResult<IEnumerable<QuizDetailsShortDTO>>> GetQuizGradesByCourseIdByStudentIdForTeacher(int courseId, int studentId)
        {
            try
            {
                var isValidCourseId = await courseRepository.IsValidCourseId(courseId);
                var isValidStudentId = await studentRepository.IsValidStudentId(studentId);
                var quizzes = await service.GetQuizGradesByCourseIdByStudentIdForTeacher(courseId, studentId);
                if (!isValidCourseId)
                    return BadRequest($"Invalid courseId: {courseId}");
                if (!isValidStudentId)
                    return BadRequest($"Invalid studentId: {studentId}");
                var mapped = mapper.Map<IEnumerable<QuizDetailsShortDTO>>(quizzes);
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                Log.Error($"Controller: QuizController , Action: GetQuizGradesByCourseIdByStudentIdForTeacher , Message: {ex.Message}");
                return StatusCode(500);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        #region Old EndPoints
        //[HttpGet("GetByIdWithAnswers/{id}")]
        //public async Task<ActionResult<Quiz>> GetByIdWithAnswers(int id)
        //{
        //    try
        //    {
        //        return Ok(await service.GetByIdWithAnswers(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Controller: QuizController , Action: GetByIdWithAnswers , Message: {ex.Message}");
        //        return StatusCode(500);
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //} 
        #endregion
    }
}
