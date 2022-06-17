using AutoMapper;
using Domain.DTOs;
using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Repository.Repositories
{
    public class AssignmentAnswerRepository : GenericRepository<AssignmentAnswer>, IAssignmentAnswerRepository
    {
        private IUnitOfWork unitOfWork { get; }
        private readonly IMapper mapper;
        public AssignmentAnswerRepository(IUnitOfWork _unitOfWork, IMapper mapper) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
            this.mapper = mapper;
        }
        //json answer + student name
        public async Task<IEnumerable<AssignmentAnswer>> GetAssignmentAnswersByAssignmentId(int assignmentId)
        {
             var a = await unitOfWork.Context.AssignmentAnswers
                .Where(aa => aa.AssignmentId == assignmentId)
                .Include(a => a.Student)
                //.Select(a => new AssignmentAnswerDetailsDTO
                //{
                //    Id = a.Id,
                //    FileName = a.FileName,
                //    PDF = a.PDF,
                //    SubmitDate = a.SubmitDate,
                //    AssignmentId = a.AssignmentId,
                //    StudentId = a.StudentId,
                //    StudentFirstName = a.Student.FirstName

                //})
                .ToListAsync();
            return a;
        }
        
        public async Task<AssignmentAnswer> GetAssignmentAnswerByStudentIdByAssignmentId(int studentId, int assignmentId)
        {
            return await unitOfWork.Context.AssignmentAnswers
                .SingleOrDefaultAsync(a => a.AssignmentId == assignmentId && a.StudentId == studentId);
        }

        public async Task<bool> IsValidAssignmentAnswerId(int id)
        {
            return await IsValidFk(a => a.Id == id);
        }

        public async Task<bool> IsNotValidAssignmentAnswerWithStudentId(int studentId, int assignmentId)
        {
            return await unitOfWork.Context.AssignmentAnswers.AnyAsync(a => a.StudentId == studentId && a.AssignmentId == assignmentId);
        }

        //public async Task<bool> IsValidAssignmentIdFk(int assignmentId)
        //{
        //    var isValid = await unitOfWork.Context.Assignments.AnyAsync(a => a.Id == assignmentId);
        //    return isValid;
        //}

        //public Task<bool> IsValidStudentIdFk(int assignmentId)
        //{
        //    throw new NotImplementedException();
        //}
        //public IQueryable<AssignmentAnswer> GetNotGradedAnswers()
        //{
        //    return unitOfWork.Context.AssignmentAnswers.Where(a => a.AssignmentGrade == null);
        //}

        //public IQueryable<AssignmentAnswer> GetByIdWithGrade(int id)
        //{
        //    return unitOfWork.Context.AssignmentAnswers
        //        .Where(a => a.Id == id)
        //        .Include(a => a.AssignmentGrade);
        //}

        //public IQueryable<AssignmentAnswer> GetByIdWithFeedback(int id)
        //{
        //    return unitOfWork.Context.AssignmentAnswers.Where(a => a.Id == id).Include(a => a.AssignmentFeedback);
        //}

        //public IQueryable<AssignmentAnswer> GetByIdWithBadge(int id)
        //{
        //    return unitOfWork.Context.AssignmentAnswers.Where(a => a.Id == id).Include(a => a.Badge);
        //}

        //public async Task<bool> isValidAssignmentAnswerFk(int id)
        //{
        //    return await unitOfWork.Context.AssignmentAnswers.AnyAsync(a => a.Id == id);
        //}
    }
}
