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
            return await unitOfWork.Context.QuizGrades.Where(g => g.QuizId == quizId).Include(g => g.Quiz).Include(g => g.Student).ToListAsync();
        }
        public async Task<IEnumerable<QuizGrade>> GetQuizGradesByCourseId(int courseId, int studentId)
        {
            return await unitOfWork.Context.QuizGrades.Where(g => g.StudentId == studentId && g.Quiz.CourseId == courseId).Include(g => g.Quiz).ToListAsync();
        }
        public async Task<QuizGrade> QuizGradeAdder(int studentId, int quizId)
        {
            var questionAnswers = await unitOfWork.Context.QuestionAnswers
                .Include(q => q.Question)
                .Where(q => q.Question.QuizId == quizId && q.StudentId == studentId)
                .ToListAsync();
            var questions = await questionRepository.GetQuestionsByQuizId(quizId);
            //var grade =  unitOfWork.Context.QuizGrades.AddAsync(new QuizGrade { Grade = 0, StudentId = studentId, QuizId = quizId }).Result;
            var quizGrade = await unitOfWork.Context.QuizGrades.SingleOrDefaultAsync(q => q.StudentId == studentId && q.QuizId == quizId);
            if (quizGrade == null) return null;
            foreach(var q in questions)
            {
                foreach(var a in questionAnswers)
                {
                    if (q.Id == a.QuestionId)
                    {
                        if (await questionAnswerRepository.CorrectQuestionAnswerOrNot(q.Id, a.Id))
                        {
                            quizGrade.AssignedGrade++;
                            await Update(quizGrade);
                        }
                        //else {
                        //    quizGrade.Grade--;
                        //    await Update(quizGrade);
                        //}
                    }
                        
                }
            }
            return quizGrade;
        }

        public async Task<bool> IsNotValidQuizGrade(int studentId, int quizId)
        {
            return await IsValidFk(g => g.StudentId == studentId && g.QuizId == quizId);
        }

        //public async Task<QuizGrade> GetQuestionAnswers(int quizId, int studentId)
        //{
        //    var questionAnswers = await unitOfWork.Context.QuestionAnswers
        //        //.Include(q => q.Question)
        //        .Where(q => q.Question.QuizId == quizId && q.StudentId == studentId)
        //        .ToListAsync();
        //    var questions = await questionRepository.GetQuestionsByQuizId(quizId);
        //   return await unitOfWork.Context.QuizGrades.SingleOrDefaultAsync(q => q.StudentId == studentId && q.QuizId == quizId);
        //}
        public async Task<int> QuizGradeAdderInt(int studentId, int quizId)
        {
            var questionAnswers = await unitOfWork.Context.QuestionAnswers
                .Include(q => q.Question)
                .Where(q => q.Question.QuizId == quizId && q.StudentId == studentId)
                .ToListAsync();
            var questions = await questionRepository.GetQuestionsByQuizId(quizId);
            var quizGrade = 0;
            foreach (var q in questions)
            {
                foreach (var a in questionAnswers)
                {
                    if (q.Id == a.QuestionId)
                    {
                        if (await questionAnswerRepository.CorrectQuestionAnswerOrNot(q.Id, a.Id))
                        {
                            quizGrade++;
                        }
                    }
                }
            }
            return quizGrade;
        }
    }
}
//answer of quiz of student
