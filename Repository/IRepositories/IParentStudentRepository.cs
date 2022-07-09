using Domain.Entities;
using ELearning_App.Repository.GenericRepositories;


namespace Repository.IRepositories
{
    public interface IParentStudentRepository : IGenericRepository<ParentStudent>
    {
        Task<bool> ExsistingParentStudentCompositeKey(int parentId, int studentId);
        Task<IEnumerable<ParentStudent>> GetUnVerifiedParentStudentRequests(int studentId);
        Task VerifyAddParentToStudentRequest(int parentId, int studentId);
    }
}
