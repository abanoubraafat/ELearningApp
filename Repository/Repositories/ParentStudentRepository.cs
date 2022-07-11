using Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.UnitOfWork;
using Repository.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ParentStudentRepository : GenericRepository<ParentStudent>, IParentStudentRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public ParentStudentRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<bool> ExsistingParentStudentCompositeKey(int parentId, int studentId)
        {
            return await IsValidFk(ps => ps.ParentId == parentId && ps.StudentId == studentId);
        }
        public async Task<IEnumerable<ParentStudent>> GetUnVerifiedParentStudentRequests(int studentId)
        {
            return await unitOfWork.Context.Set<ParentStudent>()
                .Where(ps => ps.StudentId == studentId && ps.IsVerified == false)
                .Include(ps => ps.Parent)
                .ToListAsync();
        }

        public async Task VerifyAddParentToStudentRequest(int parentId, int studentId)
        {
            var request =
                 await unitOfWork.Context.Set<ParentStudent>()
                .Where(ps => ps.ParentId == parentId && ps.StudentId == studentId)
                .FirstOrDefaultAsync();
            if(request != null)
            {
                request.IsVerified = true;
                await Update(request);
            }
        }

        
    }
}
