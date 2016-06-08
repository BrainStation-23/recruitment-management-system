using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using RecruitmentManagementSystem.Data.DbContext;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        internal ApplicationDbContext Context;
        internal DbSet<T> DbSet;

        public BaseRepository()
        {
            Context = new ApplicationDbContext();
            DbSet = Context.Set<T>();
        }

        public virtual void InsertOrUpdate(T entity)
        {
            if (entity.Id == default(int))
            {
                DbSet.Add(entity);
            }
            else
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.ApplyStateChanges();
            }
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.ApplyStateChanges();
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        public virtual void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public virtual T Find(object id)
        {
            return DbSet.Find(id);
        }

        public virtual T Find(Expression<Func<T, bool>> filter)
        {
            return DbSet.Where(filter).AsNoTracking().FirstOrDefault();
        }

        public virtual T FindIncluding(Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.AsNoTracking().FirstOrDefault();
        }

        public virtual IQueryable<T> FindAll()
        {
            return DbSet.AsNoTracking();
        }

        public virtual IQueryable<T> FindAll(Expression<Func<T, bool>> filter)
        {
            return DbSet.Where(filter).AsNoTracking();
        }

        public virtual IQueryable<T> FindAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(DbSet,
                (current, property) => current.Include(property));
        }
    }
}
