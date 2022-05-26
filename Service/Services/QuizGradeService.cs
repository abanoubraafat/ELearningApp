using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class QuizGradeService : IQuizGradeService
    {
        private readonly IQuizGradeRepository iQuizGradeRepository;

        public QuizGradeService(IQuizGradeRepository _iQuizGradeRepository)
        {
            iQuizGradeRepository = _iQuizGradeRepository;
        }


        public async Task<QuizGrade> AddQuizGrade(QuizGrade g)
        {
            return await iQuizGradeRepository.Add(g);
        }

        public async Task<QuizGrade> DeleteQuizGrade(int id)
        {
            return await iQuizGradeRepository.Delete(id);
        }

        public async Task<IEnumerable<QuizGrade>> GetAllQuizGrades()
        {
            return await iQuizGradeRepository.GetAll().ToListAsync();
        }

        public async Task<QuizGrade> GetQuizGradeById(int id)
        {
            return await iQuizGradeRepository.GetById(id);
        }

        public async Task<QuizGrade> UpdateQuizGrade(QuizGrade g)
        {
            return await iQuizGradeRepository.Update(g);
        }
    }
}
