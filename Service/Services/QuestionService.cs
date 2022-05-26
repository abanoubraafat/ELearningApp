using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository iQuestionRepository;

        public QuestionService(IQuestionRepository _iQuestionRepository)
        {
            iQuestionRepository = _iQuestionRepository;
        }
        public async Task<Question> AddQuestion(Question g)
        {
            return await iQuestionRepository.Add(g);
        }

        public async Task<Question> DeleteQuestion(int id)
        {
            return await iQuestionRepository.Delete(id);
        }

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await iQuestionRepository.GetAll().ToListAsync();
        }

        public async Task<Question> GetByIdWithAnswers(int id)
        {
            return await iQuestionRepository.GetByIdWithAnswers(id).FirstAsync();
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await iQuestionRepository.GetById(id);
        }

        public async Task<Question> UpdateQuestion(Question g)
        {
            return await iQuestionRepository.Update(g);
        }
    }
}
