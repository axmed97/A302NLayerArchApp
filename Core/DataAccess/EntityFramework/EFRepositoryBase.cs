using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EFRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using var context = new TContext();
            var addEntity = context.Entry(entity);
            addEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using var context = new TContext();
            var deleteEntity = context.Entry(entity);
            deleteEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            using var context = new TContext();

            return tracking == true
                ? context.Set<TEntity>().FirstOrDefault(expression)
                : context.Set<TEntity>().AsNoTracking().FirstOrDefault(expression);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null, bool tracking = true)
        {
            using var context = new TContext();

            return expression == null
                ? tracking == true
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().AsNoTracking().ToList()
                : tracking == tracking
                    ? [.. context.Set<TEntity>().Where(expression)]
                    : context.Set<TEntity>().AsNoTracking().Where(expression).ToList();
        }

        public void Update(TEntity entity)
        {
            using var context = new TContext();
            var updateEntity = context.Entry(entity);
            updateEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
