using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class QuestionAnswerService : IQuestionAnswerService
    {
        private readonly IQuestionAnswerRepository imageRepository;
        public QuestionAnswerService(IQuestionAnswerRepository _imageRepository)
        {
            imageRepository = _imageRepository;
        }
        public async Task<QuestionAnswer> AddQuestionAnswer(QuestionAnswer g)
        {
            return await imageRepository.Add(g);
        }

        public async Task<QuestionAnswer> DeleteQuestionAnswer(int id)
        {
            return await imageRepository.Delete(id);
        }

        public async Task<IEnumerable<QuestionAnswer>> GetAllQuestionAnswers()
        {
            return await imageRepository.GetAll().ToListAsync();
        }

        public async Task<QuestionAnswer> GetQuestionAnswerById(int id)
        {
            return await imageRepository.GetById(id);
        }

        public async Task<QuestionAnswer> UpdateQuestionAnswer(QuestionAnswer g)
        {
            return await imageRepository.Update(g);
        }
    }
}
