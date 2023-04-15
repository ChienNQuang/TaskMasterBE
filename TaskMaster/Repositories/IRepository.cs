using TaskMaster.Models.Entities;

namespace TaskMaster.Repositories;

public interface IRepository<TEntity, in TKey>
where TEntity : Entity<TKey>
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Remove(TKey id);
}