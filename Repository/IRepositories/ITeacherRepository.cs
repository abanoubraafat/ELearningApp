using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface ITeacherRepository : IGenericRepository <Teacher>
    {
        //IQueryable<Teacher> GetAllWithCourses();

        //IQueryable<Teacher> GetByIdWithCourses(int id);
        Task<bool> IsValidTeacherId(int id);

    }
}
