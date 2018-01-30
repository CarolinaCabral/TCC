using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CoffeeExperience.Data.Repositories
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private Contexto _contexto;
        protected readonly DbSet<T> dbset;

        public void setLazyLoading(bool isToLazyLoading)
        {
            _contexto.Configuration.LazyLoadingEnabled = isToLazyLoading;
        }
        protected GenericRepository(IGetContext getContext)
        {
            _unitOfWork = getContext;
            dbset = getContexto.Set<T>();
        }

        protected IGetContext _unitOfWork
        {
            get;
            private set;
        }

        protected Contexto getContexto
        {
            get { return _contexto ?? (_contexto = _unitOfWork.Get()); }
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            _contexto.Entry(entity).State = EntityState.Modified;
        }


        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual T GetByIdAsNoTracking(int id)
        {
            var entity = dbset.Find(id);
            _contexto.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public virtual bool Any(int id)
        {
            T TEntity = GetById(id);
            return TEntity == null ? false : true;
        }

        public virtual bool Any(Expression<Func<T, bool>> where)
        {
            T TEntity = Get(where);
            return TEntity == null ? false : true;
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbset;
        }
        

        public virtual IQueryable<T> GetAll(Func<T, object> order, int pageSize, int pageIndex, out int totalPages)
        {
            totalPages = dbset.Count();
            return dbset.OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
        }

        public virtual IQueryable<T> GetAllDescending(Func<T, object> order, int pageSize, int pageIndex, out int totalPages)
        {
            totalPages = dbset.Count();
            return dbset.OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }

        public virtual IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> where)
        {
            var entity = dbset.Where(where);
            _contexto.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where, Func<T, object> order, int pageSize, int pageIndex, out int totalPages)
        {
            totalPages = dbset.Where(where).Count();
            return dbset.Where(where).OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }

        public T GetWithIncludes(Expression<Func<T, bool>> where, params Expression<Func<T, Object>>[] paths)
        {
            this.setLazyLoading(false);

            var query = dbset.Where(where);

            foreach (var path in paths)
            {
                query = query.Include(path);
            }

            return query.FirstOrDefault();
        }        

        public T GetAsNoTracking(Expression<Func<T, bool>> where)
        {
            var entity = dbset.Where(where).FirstOrDefault<T>();
            _contexto.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        
        public int Count(Expression<Func<T, bool>> where)
        {
            return dbset.Count(where);
        }

        public void Reload(T entity)
        {
            _contexto.Entry<T>(entity).Reload();
        }

        public void Reload(T entity, Expression<Func<T, object>> attribute)
        {
            _contexto.Entry<T>(entity).Reference(attribute).Load();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}

