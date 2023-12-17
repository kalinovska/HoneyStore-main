using System.Linq.Expressions;

namespace HoneyStore.DataAccess.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        Task<TEntity> GetAsync(int id);

        Task<ICollection<TEntity>> GetAllAsync();

        Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(ICollection<TEntity> entities);

        Task RemoveAsync(TEntity entity);

        Task RemoveRangeAsync(ICollection<TEntity> entities);

        Task UpdateAsync(int id, TEntity entity);
    }
}
