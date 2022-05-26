using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class QuizAnswerService : IQuizAnswerService
    {
        private readonly IQuizAnswerRepository iQuizAnswerRepository;
        public QuizAnswerService(IQuizAnswerRepository _iQuizAnswerRepository)
        {
            this.iQuizAnswerRepository = _iQuizAnswerRepository;
        }
        public async Task<QuizAnswer> AddQuizAnswer(QuizAnswer a)
        {
            return await iQuizAnswerRepository.Add(a);
        }

        public async Task<QuizAnswer> DeleteQuizAnswer(int id)
        {
            return await iQuizAnswerRepository.Delete(id);
        }

        public async Task<IEnumerable<QuizAnswer>> GetAllQuizAnswers()
        {
            return await iQuizAnswerRepository.GetAll().ToListAsync();
        }

        public async Task<QuizAnswer> GetByIdWithGrade(int id)
        {
            return await iQuizAnswerRepository.GetByIdWithGrade(id).FirstAsync();
        }

        public async Task<QuizAnswer> GetQuizAnswerById(int id)
        {
            return await iQuizAnswerRepository.GetById(id);
        }

        public async Task<QuizAnswer> UpdateQuizAnswer(QuizAnswer a)
        {
            return await iQuizAnswerRepository.Update(a);
        }
    }
}
