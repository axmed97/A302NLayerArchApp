using Core.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class, IEntity
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity Get(Expression<Func<TEntity, bool>> expression, bool tracking = true);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null, bool tracking = true);
    }
}
