using TaskMaster.Models.Entities;

namespace TaskMaster.Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity, TKey> GetRequiredRepository<TEntity, TKey>() where TEntity : Entity<TKey>;
    Task<int> CommitAsync();
}