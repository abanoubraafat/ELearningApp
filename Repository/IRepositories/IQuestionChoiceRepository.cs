using Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IQuestionChoiceRepository : IGenericRepository<QuestionChoice>
    {
        Task<bool> IsValidQuestionChoiceFk(int id);
        Task<IEnumerable<QuestionChoice>> GetQuestionChoicesByQuestionId(int questionId);
    }
}
