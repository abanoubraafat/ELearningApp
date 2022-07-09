using Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface ICourseStudentRepository : IGenericRepository<CourseStudent>
    {
        Task<IEnumerable<CourseStudent>> GetUnVerifiedCourseStudentRequests(int teacherId);
        Task VerifyAddCourseToStudentRequest(int courseId, int studentId);
        Task<bool> ExsistingCourseStudentCompositeKey(int courseId, int studentId);
   

    }
}
