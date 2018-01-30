using System;
using System.Linq;
using System.Linq.Expressions;

namespace CoffeeExperience.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(int id);
        T GetByIdAsNoTracking(int id);
        bool Any(int id);
        bool Any(Expression<Func<T, bool>> where);
        T Get(Expression<Func<T, bool>> where);
        T GetWithIncludes(Expression<Func<T, bool>> where, params Expression<Func<T, Object>>[] paths);
        T GetAsNoTracking(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Func<T, object> order, int pageSize, int pageIndex, out int totalPages);
        IQueryable<T> GetAllDescending(Func<T, object> order, int pageSize, int pageIndex, out int totalPages);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> where);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where, Func<T, object> order, int pageSize, int pageIndex, out int totalPages);
        int Count(Expression<Func<T, bool>> where);
        void Reload(T entity);
        void Reload(T entity, Expression<Func<T, object>> attribute);
        void Dispose();
    }
}
