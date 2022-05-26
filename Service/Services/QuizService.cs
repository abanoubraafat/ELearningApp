using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository iQuizRepository;

        public QuizService(IQuizRepository _iQuizRepository)
        {
            iQuizRepository = _iQuizRepository;
        }
        public async Task<Quiz> AddQuiz(Quiz q)
        {
            return await iQuizRepository.Add(q);
        }

        public async Task<Quiz> DeleteQuiz(int id)
        {
            return await iQuizRepository.Delete(id);
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizes()
        {
            return await iQuizRepository.GetAll().ToListAsync();
        }

        public async Task<Quiz> GetByIdWithAnswers(int id)
        {
            return await iQuizRepository.GetByIdWithAnswers(id).FirstAsync();
        }

        public async Task<Quiz> GetQuizById(int id)
        {
            return await iQuizRepository.GetById(id);
        }

        public async Task<Quiz> UpdateQuiz(Quiz q)
        {
            return await iQuizRepository.Update(q);
        }
    }
}
