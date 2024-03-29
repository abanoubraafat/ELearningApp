﻿using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;

namespace ELearning_App.Repository.IRepositories
{
    public interface IAssignmentAnswerRepository : IGenericRepository<AssignmentAnswer>
    {
        //public IQueryable<AssignmentAnswer> GetNotGradedAnswers();
        //public IQueryable<AssignmentAnswer> GetByIdWithGrade(int id);
        //public IQueryable<AssignmentAnswer> GetByIdWithFeedback(int id);
        //public IQueryable<AssignmentAnswer> GetByIdWithBadge(int id);
        //Task<bool> isValidAssignmentAnswerFk(int id);
        Task<IEnumerable<AssignmentAnswer>> GetAssignmentAnswersByAssignmentId(int assignmentId);
        Task<AssignmentAnswer> GetAssignmentAnswerByStudentIdByAssignmentId(int studentId, int assignmentId);
        //Task<bool> IsValidAssignmentIdFk(int assignmentId);
        //Task<bool> IsValidStudentIdFk(int assignmentId);
        Task<bool> IsValidAssignmentAnswerId(int id);
        Task<bool> IsNotValidAssignmentAnswerWithStudentId(int studentId, int assignmentId);
        Task<bool> IsSubmittedAssignmentAnswer(int assignmentId, int studentId);
        Task<List<AssignmentAnswer>> GetAssignmentAnswersByListOfIds(int[] ids);
        Task<int?> GetIntAssignmentGrade(int assignmentId, int studentId);
        Task<IEnumerable<AssignmentAnswer>> GetAssignmentAnswersByCourseId(int courseId, int studentId);
    }
}
