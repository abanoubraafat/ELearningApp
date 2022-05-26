using ELearning_App.Domain.Entities;
using ELearning_App.Repository.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearning_App.Repository.IRepositories
{
    public interface IResourceRepository : IGenericRepository<Resource>
    {
        Task<bool> IsValidResourceId(int id);

    }
}

