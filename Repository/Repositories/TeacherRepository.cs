using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public TeacherRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<bool> IsValidTeacherId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        //public IQueryable<Teacher> GetAllWithCourses()
        //{
        //    return unitOfWork.Context.Teachers.Include(t => t.Courses);
        //}

        //public IQueryable<Teacher> GetByIdWithCourses(int id)
        //{
        //    return unitOfWork.Context.Teachers.Where(t => t.Id == id).Include(t => t.Courses);
        //}
    }
}
