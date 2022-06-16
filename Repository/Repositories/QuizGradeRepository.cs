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
        private IQuestionAnswerRepository questionAnswerRepository { get; }
        private IQuestionRepository questionRepository { get; }
        public QuizGradeRepository(IUnitOfWork _unitOfWork, IQuestionAnswerRepository questionAnswerRepository, IQuestionRepository questionRepository) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
            this.questionAnswerRepository = questionAnswerRepository;
            this.questionRepository = questionRepository;
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

        public async Task<QuizGrade> GetQuizGradeByQuizIdByStudentId(int quizId, int studentId)
        {
            return await unitOfWork.Context.QuizGrades
                .SingleOrDefaultAsync(g => g.QuizId == quizId && g.StudentId == studentId);
        }

        public async Task<IEnumerable<QuizGrade>> GetQuizGradesByQuizId(int quizId)
        {
            return await unitOfWork.Context.QuizGrades.Where(g => g.QuizId == quizId).ToListAsync();
        }
        public async Task<QuizGrade> QuizGradeAdder(int gradeId, int questionId, int studentId, int quizId)
        {
            var questionAnswers = unitOfWork.Context.QuestionAnswers
                .Include(q => q.Question)
                .Where(q => q.Question.QuizId == quizId && q.StudentId == studentId)
                .ToListAsync().Result;
            var questions = questionRepository.GetQuestionsByQuizId(quizId).Result;
            //var grade =  unitOfWork.Context.QuizGrades.AddAsync(new QuizGrade { Grade = 0, StudentId = studentId, QuizId = quizId }).Result;
            var quizGrade = unitOfWork.Context.QuizGrades
                .SingleOrDefaultAsync(q => q.StudentId == studentId && q.QuizId == quizId).Result;
            foreach(var q in questions)
            {
                foreach(var a in questionAnswers)
                {
                    if (q.Id == a.QuestionId)
                    {
                        if (await questionAnswerRepository.CorrectQuestionAnswerOrNot(q.Id, a.Id))
                        {
                            quizGrade.Grade++;
                            await Update(quizGrade);
                        }
                    }
                        
                }
            }
            return quizGrade;
        }
    }
}
//answer of quiz of student
