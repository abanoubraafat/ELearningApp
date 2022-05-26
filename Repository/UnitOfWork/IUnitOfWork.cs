using ELearning_App.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ELearning_App.Repository.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        ELearningContext Context { get; }

        Task Commit();
    }
}
