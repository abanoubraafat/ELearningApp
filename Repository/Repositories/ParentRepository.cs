using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class ParentRepository : GenericRepository<Parent>, IParentRepository
    {
        public IUnitOfWork unitOfWork { get; }
        public ParentRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        //public async Task<Parent> GetParentByStudentId(int studentId)
        //{
        //    return await unitOfWork.Context.Parents
        //        .FirstAsync(p => p.StudentId == studentId);
        //}
        public async Task<bool> IsValidParentId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        //public async Task<Parent> AddStudentsByEmail(int parentId, string studentEmail)
        //{
        //    var student = await unitOfWork.Context.Students.FirstAsync(s => s.EmailAddress == studentEmail);
        //    var parent = await unitOfWork.Context.Parents.FirstAsync(p => p.Id == parentId);
        //    if (student == null || parent == null)
        //        return null;
        //    parent.Students.Add(student);
        //    await Update(parent);
        //    return parent.Include
        //}
        //public IQueryable<Parent> GetAllWithStudentWithCoursesWithGrades()
        //{
        //    return unitOfWork.Context.Parents;
        //    //.Include(t => t.Students); //not finished
        //}

        //public IQueryable<Parent> GetByIdWithStudentWithCoursesWithGrades(int id)
        //{
        //    return unitOfWork.Context.Parents.Where(p => p.Id == id);
        //    //.Include(t => t.Students); //not finished
        //}
    }
}
