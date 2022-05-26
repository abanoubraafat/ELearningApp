using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IParentRepository :IGenericRepository<Parent>
    {
        //IQueryable<Parent> GetAllWithStudentWithCoursesWithGrades();
        //IQueryable<Parent> GetByIdWithStudentWithCoursesWithGrades(int id);
        //Task<Parent> GetParentByStudentId(int studentId);
        Task<bool> IsValidParentId(int id);

    }
}
