using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository iLessonRepository;

        public LessonService(ILessonRepository _iLessonRepository)
        {
            iLessonRepository = _iLessonRepository;
        }
        public async Task<Lesson> AddLesson(Lesson g)
        {
            return await iLessonRepository.Add(g);
        }

        public async Task<Lesson> DeleteLesson(int id)
        {
            return await iLessonRepository.Delete(id);
        }

        public async Task<IEnumerable<Lesson>> GetAllLessons()
        {
            return await iLessonRepository.GetAll().ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> GetByIdWithContent(int id)
        {
            return await iLessonRepository.GetByIdWithContent(id)
                  .Select(l => new Lesson
                  {
                      Id = l.Id,
                      Title = l.Title,
                      Contents = l.Contents
                  })
                  .ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> GetByIdWithNotes(int id)
        {
            return await iLessonRepository.GetByIdWithContent(id)
                              .Select(l => new Lesson
                              {
                                  Id = l.Id,
                                  Title = l.Title,
                                  Notes = l.Notes
                              })
                              .ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> GetByIdWithQuestions(int id)
        {
            return await iLessonRepository.GetByIdWithContent(id)
                              .Select(l => new Lesson
                              {
                                  Id = l.Id,
                                  Title = l.Title,
                                  Questions = l.Questions
                              })
                              .ToListAsync();
        }

        public async Task<Lesson> GetLessonById(int id)
        {
            return await iLessonRepository.GetById(id);
        }

        public async Task<Lesson> UpdateLesson(Lesson g)
        {
            return await iLessonRepository.Update(g);
        }
    }
}
