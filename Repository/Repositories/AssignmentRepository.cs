using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ELearning_App.Repository.Repositories
{
    public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public AssignmentRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByCourseId(int courseId)
        {
            return await unitOfWork.Context.Assignments
                .Where(a => a.CourseId == courseId)
                .ToListAsync();
        }
        public async Task<bool> IsValidAssignmentId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByCourseIdForStudent(int courseId)
        {
            return await unitOfWork.Context.Assignments
                .Where(a => a.CourseId == courseId)
                .Select(a => new Assignment
                {
                    CourseId = a.CourseId,
                    Id = a.Id,
                    Title = a.Title,
                    EndTime = a.EndTime
                })
                .ToListAsync();
        }

        //public IQueryable<Assignment> GetByIdWithCourses(int id1, int id2)
        //{
        //    //C_id = unitOfWork.Context.Assignments.Where(a => a.Id == id).Select(a => a.CourseId).First();
        //    return unitOfWork.Context.Assignments.Where(a => a.CourseId == id1 && a.Id == id2);
        //}

        //public IQueryable<Assignment> GetAllWithAssignmentAnswers()
        //{
        //    return unitOfWork.Context.Assignments.Include(a => a.AssignmentAnswers);
        //}

        //public IQueryable<Assignment> GetByIdWithAssignmentAnswers(int id)
        //{
        //    return unitOfWork.Context.Assignments.Where(a => a.Id == id).Include(a => a.AssignmentAnswers);
        //}
    }
}


