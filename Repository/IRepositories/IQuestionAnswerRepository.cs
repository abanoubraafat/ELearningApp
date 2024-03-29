﻿using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IQuestionAnswerRepository : IGenericRepository<QuestionAnswer>
    {
        //Task<IEnumerable<QuestionAnswer>> GetQuestionAnswersByQuestionId(int questionId);
        //Task<QuestionAnswer> GetQuestionAnswerByQuestionIdByStudentId(int questionId, int studentId);
        Task<bool> IsValidQuestionAnswerId(int id);
        Task<IEnumerable<QuestionAnswer>> GetQuestionAnswersByQuestionId(int questionId);
        Task<QuestionAnswer> GetQuestionAnswerByQuestionIdByStudentId(int questionId, int studentId);
        Task<bool> CorrectQuestionAnswerOrNot(int questionId, int questionAnswerId);
        Task<bool> IsNotValidQuestionAnswer(int studentId, int questionId);
    }
}
