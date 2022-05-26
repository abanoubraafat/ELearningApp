﻿//using ELearning_App.Domain.Entities;
//using ELearning_App.Repository.GenericRepositories;
//using ELearning_App.Repository.IRepositories;
//using ELearning_App.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ELearning_App.Repository.Repositories
//{
//    public class QuestionAnswerRepository : GenericRepository<QuestionAnswer>, IQuestionAnswerRepository
//    {
//        private IUnitOfWork unitOfWork { get; }
//        public QuestionAnswerRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
//        {
//            unitOfWork = _unitOfWork;
//        }

//        public async Task<IEnumerable<QuestionAnswer>> GetQuestionAnswersByQuestionId(int questionId)
//        {
//            return await unitOfWork.Context.QuestionAnswers
//                .Where(q => q.QuestionId == questionId)
//                .Include(q => q.Student)
//                .ToListAsync();
//        }
//        public async Task<QuestionAnswer> GetQuestionAnswerByQuestionIdByStudentId(int questionId, int studentId)
//        {
//            return await unitOfWork.Context.QuestionAnswers
//                .FirstAsync(q => q.QuestionId == questionId && q.StudentId == studentId);
//        }
//        public async Task<bool> IsValidQuestionAnswerId(int id)
//        {
//            return await IsValidFk(a => a.Id == id);
//        }
//    }
//}
