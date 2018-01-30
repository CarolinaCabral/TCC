using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.Domain.Interfaces.Services
{
    public interface IGenericService <T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(int id);
        T Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Func<T, object> order, int pageSize, int pageIndex, out int totalPages);
        IQueryable<T> GetAllDescending(Func<T, object> order, int pageSize, int pageIndex, out int totalPages);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where, Func<T, object> order, int pageSize, int pageIndex, out int totalPages);
        int Count(Expression<Func<T, bool>> where);
        void Reload(T entity);
        void Reload(T entity, Expression<Func<T, object>> attribute);
        void Dispose();

    }
}
