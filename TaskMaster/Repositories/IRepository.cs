using System.Linq.Expressions;
using TaskMaster.Models.Entities;

namespace TaskMaster.Repositories;

public interface IRepository<TEntity, in TKey>
where TEntity : Entity<TKey>
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Remove(TKey id);
}