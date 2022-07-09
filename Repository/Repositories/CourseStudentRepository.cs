using Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CourseStudentRepository : GenericRepository<CourseStudent>, ICourseStudentRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public CourseStudentRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<CourseStudent>> GetUnVerifiedCourseStudentRequests(int teacherId)
        {
            return await unitOfWork.Context.Set<CourseStudent>()
                .Where(cs => cs.Course.TeacherId == teacherId && cs.IsVerified == false)
                .Include(cs => cs.Student)
                .Include(cs => cs.Course)
                .Select(cs => new CourseStudent
                {
                    StudentId = cs.StudentId,
                    CourseId = cs.CourseId,
                    Student = cs.Student,
                    Course = cs.Course
                })
                .ToListAsync();
        }

        public async Task VerifyAddCourseToStudentRequest(int courseId, int studentId)
        {
            var request =
                 await unitOfWork.Context.Set<CourseStudent>()
                .Where(cs => cs.CourseId == courseId && cs.StudentId == studentId)
                .FirstOrDefaultAsync();
            if (request != null)
            {
                request.IsVerified = true;
                await Update(request);
            }
        }

        public async Task<bool> ExsistingCourseStudentCompositeKey(int courseId, int studentId)
        {
            return await IsValidFk(cs => cs.CourseId == courseId && cs.StudentId == studentId);
        }

    }
}
