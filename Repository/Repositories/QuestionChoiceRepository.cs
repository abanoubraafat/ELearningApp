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
    public class QuestionChoiceRepository : GenericRepository<QuestionChoice>, IQuestionChoiceRepository
    {
        private IUnitOfWork unitOfWork { get; }
        public QuestionChoiceRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<bool> IsValidQuestionChoiceFk(int id)
        {
            return await IsValidFk(c => c.Id == id);
        }

        public async Task<IEnumerable<QuestionChoice>> GetQuestionChoicesByQuestionId(int questionId)
        {
            return await unitOfWork.Context.QuestionChoices
                .Where(q => q.QuestionId == questionId)
                .ToListAsync();
        }
    }
}
