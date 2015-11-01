using System;
using System.Linq;
using System.Linq.Expressions;

namespace RecruitmentManagementSystem.Data.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        void Save();

        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        TEntity FindById(object id);

        IQueryable<TEntity> FindAll();
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> FindAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}