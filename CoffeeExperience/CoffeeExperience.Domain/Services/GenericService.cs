using CoffeeExperience.Domain.Interfaces.Repositories;
using CoffeeExperience.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.Domain.Services
{
   
        public class GenericService<TEntity> : IDisposable, IGenericService<TEntity> where TEntity : class
        {
            private readonly IGenericRepository<TEntity> _repository;

            public GenericService(IGenericRepository<TEntity> repository)
            {
                _repository = repository;
            }

            public void Add(TEntity entity)
            {
                _repository.Add(entity);
            }

            public int Count(Expression<Func<TEntity, bool>> where)
            {
                return _repository.Count(where);
            }

            public void Delete(Expression<Func<TEntity, bool>> where)
            {
                _repository.Delete(where);
            }

            public void Delete(TEntity entity)
            {
                _repository.Delete(entity);
            }

            public void Dispose()
            {
                _repository.Dispose();
            }

            public TEntity Get(Expression<Func<TEntity, bool>> where)
            {
                return _repository.Get(where);
            }

            public IQueryable<TEntity> GetAll()
            {
                return _repository.GetAll();
            }

            public IQueryable<TEntity> GetAll(Func<TEntity, object> order, int pageSize, int pageIndex, out int totalPages)
            {
                return _repository.GetAll(order, pageSize, pageIndex, out totalPages);
            }

            public IQueryable<TEntity> GetAllDescending(Func<TEntity, object> order, int pageSize, int pageIndex, out int totalPages)
            {
                return _repository.GetAllDescending(order, pageSize, pageIndex, out totalPages);
            }

            public virtual TEntity GetById(int id)
            {
                return _repository.GetById(id);
            }

            public IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
            {
                return _repository.GetMany(where);
            }

            public IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, Func<TEntity, object> order, int pageSize, int pageIndex, out int totalPages)
            {
                return _repository.GetMany(where, order, pageSize, pageIndex, out totalPages);
            }

            public void Reload(TEntity entity)
            {
                _repository.Reload(entity);

            }

            public void Reload(TEntity entity, Expression<Func<TEntity, object>> attribute)
            {
                _repository.Reload(entity, attribute);
            
            }

            public void Update(TEntity entity)
            {
                _repository.Update(entity);
            }
        }
    
}
