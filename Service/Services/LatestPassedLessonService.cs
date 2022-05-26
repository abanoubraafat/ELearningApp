using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class LatestPassedLessonService : ILatestPassedLessonService
    {
        private readonly ILatestPassedLessonRepository iLatestPassedLessonRepository;

        public LatestPassedLessonService(ILatestPassedLessonRepository _iLatestPassedLessonRepository)
        {
            iLatestPassedLessonRepository = _iLatestPassedLessonRepository;
        }
        public async Task<LatestPassedLesson> AddLatestPassedLesson(LatestPassedLesson g)
        {
            return await iLatestPassedLessonRepository.Add(g);
        }

        public async Task<LatestPassedLesson> DeleteLatestPassedLesson(int id)
        {
            return await iLatestPassedLessonRepository.Delete(id);
        }

        public async Task<IEnumerable<LatestPassedLesson>> GetAllLatestPassedLessons()
        {
            return await iLatestPassedLessonRepository.GetAll().ToListAsync();
        }

        public async Task<LatestPassedLesson> GetLatestPassedLessonById(int id)
        {
            return await iLatestPassedLessonRepository.GetById(id);
        }

        public async Task<LatestPassedLesson> UpdateLatestPassedLesson(LatestPassedLesson g)
        {
            return await iLatestPassedLessonRepository.Update(g);
        }
    }
}
