﻿using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IQuizGradeRepository : IGenericRepository<QuizGrade>
    {
        //Task<QuizGrade> GetQuizGradeByQuizAnswerId(int quizAnswerId);
        Task<bool> IsValidQuizGradeId(int id);
        Task<QuizGrade> GetQuizGradeByQuizIdByStudentId(int quizId, int studentId);
        Task<IEnumerable<QuizGrade>> GetQuizGradesByQuizId(int quizId);
        Task<QuizGrade> QuizGradeAdder(int studentId, int quizId);
        Task<bool> IsNotValidQuizGrade(int studentId, int quizId);
        Task<int> QuizGradeAdderInt(int studentId, int quizId);
        Task<IEnumerable<QuizGrade>> GetQuizGradesByCourseId(int courseId, int studentId);

    }
}
