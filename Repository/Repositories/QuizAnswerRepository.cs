//using ELearning_App.Domain.Entities;
//using ELearning_App.Repository.GenericRepositories;
//using ELearning_App.Repository.IRepositories;
//using ELearning_App.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ELearning_App.Repository.Repositories
//{
//    public class QuizAnswerRepository : GenericRepository<QuizAnswer>, IQuizAnswerRepository
//    {
//        private IUnitOfWork unitOfWork { get; }
//        public QuizAnswerRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
//        {
//            unitOfWork = _unitOfWork;
//        }

//        public async Task<IEnumerable<QuizAnswer>> GetQuizAnswersByQuestionId(int quizId)
//        {
//            return await unitOfWork.Context.QuizAnswers
//                .Where(q => q.QuizId == quizId)
//                .Include(q => q.Student)
//                .ToListAsync();
//        }

//        public async Task<QuizAnswer> GetQuizAnswerByQuestionIdByStudentId(int quizId, int studentId)
//        {
//            return await unitOfWork.Context.QuizAnswers
//                .FirstAsync(q => q.QuizId == quizId && q.StudentId == studentId);
//        }
//        public async Task<bool> IsValidQuizAnswerId(int id)
//        {
//            return await IsValidFk(a => a.Id == id);
//        }

//        //public IQueryable<QuizAnswer> GetByIdWithGrade(int id)
//        //{
//        //    return unitOfWork.Context.QuizAnswers.Where(a => a.Id == id).Include(a => a.QuizGrade);
//        //}
//    }
//}
