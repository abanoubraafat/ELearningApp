using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IQuizRepository : IGenericRepository<Quiz>
    {
        //IQueryable<Quiz> GetByIdWithAnswers(int id);
        Task<IEnumerable<Quiz>> GetQuizzesByCourseId(int courseId);
        Task<bool> IsValidQuizId(int id);

    }
}
