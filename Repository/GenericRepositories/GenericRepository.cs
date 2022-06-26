using Domain.Consts;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace ELearning_App.Repository.GenericRepositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private IUnitOfWork unitOfWork { get; }
        // Dependency Injection
        public GenericRepository (IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public TEntity Add(TEntity entity)
        {
            unitOfWork.Context.Set<TEntity>().Add(entity);
            unitOfWork.Commit();
            return entity;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await unitOfWork.Context.Set<TEntity>().AddAsync(entity);
            await unitOfWork.Commit();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            TEntity entity = await GetByIdAsync(id);
            unitOfWork.Context.Set<TEntity>().Remove(entity);
            await unitOfWork.Commit();
            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return unitOfWork.Context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await unitOfWork.Context.Set<TEntity>().ToListAsync();
        }
        public TEntity GetById(int id)
        {
            return unitOfWork.Context.Set<TEntity>().Find(id);
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await unitOfWork.Context.Set<TEntity>().FindAsync(id);
        }
       
        public async Task<TEntity> Update(TEntity entity)
        {
            unitOfWork.Context.Update(entity);
            await unitOfWork.Commit();
            return entity;
        }
        //public async Task<bool> isValidFk(Expression<Func<TEntity, bool>> criteria, int id)
        //{
        //    return await unitOfWork.Context.Set<TEntity>().AnyAsync(criteria);
        //}

        //public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        //{
        //    IQueryable<T> query = _context.Set<T>();

        //    if (includes != null)
        //        foreach (var incluse in includes)
        //            query = query.Include(incluse);

        //    return query.SingleOrDefault(criteria);
        //}

        //public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        //{
        //    IQueryable<T> query = _context.Set<T>();

        //    if (includes != null)
        //        foreach (var incluse in includes)
        //            query = query.Include(incluse);

        //    return await query.SingleOrDefaultAsync(criteria);
        //}

        //public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        //{
        //    IQueryable<T> query = _context.Set<T>();

        //    if (includes != null)
        //        foreach (var include in includes)
        //            query = query.Include(include);

        //    return query.Where(criteria).ToList();
        //}

        //public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
        //{
        //    return _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
        //}

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> criteria,
            Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<TEntity> query = unitOfWork.Context.Set<TEntity>().Where(criteria);

            
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.ToList();
        }

        //public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        //{
        //    IQueryable<T> query = _context.Set<T>();

        //    if (includes != null)
        //        foreach (var include in includes)
        //            query = query.Include(include);

        //    return await query.Where(criteria).ToListAsync();
        //}

        //public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
        //{
        //    return await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
        //}

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria,
            Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<TEntity> query = unitOfWork.Context.Set<TEntity>().Where(criteria);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> IsValidFk(Expression<Func<TEntity, bool>> criteria)
        {
            return await unitOfWork.Context.Set<TEntity>().AnyAsync(criteria);
        }

        //public T Add(T entity)
        //{
        //    _context.Set<T>().Add(entity);
        //    return entity;
        //}

        //public async Task<T> AddAsync(T entity)
        //{
        //    await _context.Set<T>().AddAsync(entity);
        //    return entity;
        //}

        //public IEnumerable<T> AddRange(IEnumerable<T> entities)
        //{
        //    _context.Set<T>().AddRange(entities);
        //    return entities;
        //}

        //public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        //{
        //    await _context.Set<T>().AddRangeAsync(entities);
        //    return entities;
        //}

        //public T Update(T entity)
        //{
        //    _context.Update(entity);
        //    return entity;
        //}

        //public void Delete(T entity)
        //{
        //    _context.Set<T>().Remove(entity);
        //}

        //public void DeleteRange(IEnumerable<T> entities)
        //{
        //    _context.Set<T>().RemoveRange(entities);
        //}

        //public void Attach(T entity)
        //{
        //    _context.Set<T>().Attach(entity);
        //}

        //public void AttachRange(IEnumerable<T> entities)
        //{
        //    _context.Set<T>().AttachRange(entities);
        //}

        //public int Count()
        //{
        //    return _context.Set<T>().Count();
        //}

        //public int Count(Expression<Func<T, bool>> criteria)
        //{
        //    return _context.Set<T>().Count(criteria);
        //}

        //public async Task<int> CountAsync()
        //{
        //    return await _context.Set<T>().CountAsync();
        //}

        //public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        //{
        //    return await _context.Set<T>().CountAsync(criteria);
        //}
        public async Task AddMultipleAsync(List<TEntity> entity)
        {
            await unitOfWork.Context.Set<TEntity>().AddRangeAsync(entity);
            await unitOfWork.Commit();
        }
    }
}
