using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class QuizGradeRepository : GenericRepository<QuizGrade>, IQuizGradeRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public QuizGradeRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        //public async Task<QuizGrade> GetQuizGradeByQuizAnswerId(int quizAnswerId)
        //{
        //    return await unitOfWork.Context.QuizGrades
        //        .FirstAsync(q => q.QuizAnswerId == quizAnswerId);
        //}
        public async Task<bool> IsValidQuizGradeId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }
    }
}
