using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public QuizRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesByCourseId(int courseId)
        {
            return await unitOfWork.Context.Quizzes
                .Where(x => x.CourseId == courseId)
                .ToListAsync();
        }
        public async Task<bool> IsValidQuizId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        //public IQueryable<Quiz> GetByIdWithAnswers(int id)
        //{
        //    return unitOfWork.Context.Quizzes.Where(q => q.Id == id).Include(q => q.QuizAnswers);
        //}
    }
}
