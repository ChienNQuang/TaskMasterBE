using TaskMaster.Models.Entities;

namespace TaskMaster.Repositories;

public interface IRepository<TEntity, in TKey>
where TEntity : Entity<TKey>
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<TEntity> AddAsync(TEntity entity);
    TEntity UpdateAsync(TEntity entity);
    TEntity RemoveAsync(TKey id);
}