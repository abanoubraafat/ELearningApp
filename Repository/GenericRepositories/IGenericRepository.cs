using Domain.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace ELearning_App.Repository.GenericRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        TEntity Add(TEntity entity);
        Task/*<TEntity>*/ AddAsync(TEntity entity);

        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);

        IQueryable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task/*<TEntity>*/ Delete(int id);

        Task<TEntity> Update(TEntity entity);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> criteria,
         Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria,
        Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

        Task<bool> IsValidFk(Expression<Func<TEntity, bool>> criteria);

        //Task<bool> isValidFk(Expression<Func<TEntity, bool>> criteria, int id);
        Task AddMultipleAsync(List<TEntity> entity);
        Task<List<TEntity>> UpdateMultiple(List<TEntity> entity);
    }
}
