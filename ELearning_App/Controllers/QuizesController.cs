﻿using System;
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
        private readonly IMapper mapper;
        public QuizesController(IQuizRepository _service, IMapper mapper, ICourseRepository courseRepository)
        {
            service = _service;
            this.mapper = mapper;
            new Logger();
            this.courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            try
            {
                return Ok(await service.GetAllAsync());
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
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            try
            {
                var quiz = await service.GetByIdAsync(id);
                if (quiz == null)
                    return NotFound($"Invalid quizId : {id}");
                return Ok(quiz);
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
                quiz.Grade = q.Grade;
                quiz.StartTime = q.StartTime;
                quiz.EndTime = q.EndTime;
                quiz.PostTime = q.PostTime;
                //quiz.CourseId = q.CourseId;
                return Ok(await service.Update(quiz));
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
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzesByCourseId(int courseId)
        {
            try
            {
                var isValidCourseId = await courseRepository.IsValidCourseId(courseId);
                var quizzes = await service.GetQuizzesByCourseId(courseId);
                if (!isValidCourseId)
                    return BadRequest($"Invalid courseId: {courseId}");
                if (quizzes.Count() == 0)
                    return NotFound($"There're No Quizzes with such courseId ; {courseId}");
                return Ok(quizzes);
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
