using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public QuestionRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        //public async Task<IEnumerable<Question>> GetQuestionsByLessonId(int lessonId)
        //{
        //    return await unitOfWork.Context.Questions
        //        .Where(q => q.LessonId == lessonId)
        //        .ToListAsync();
        //}
        public async Task<bool> IsValidQuestionId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        public async Task<IEnumerable<Question>> GetQuestionsByQuizId(int quizId)
        {
            return await unitOfWork.Context.Questions.Where(q => q.QuizId == quizId).ToListAsync();
        }
        //public IQueryable<Question> GetByIdWithAnswers(int id)
        //{
        //    return unitOfWork.Context.Questions.Where(q => q.Id == id).Include(q => q.QuestionAnswers);
        //}

    }
}