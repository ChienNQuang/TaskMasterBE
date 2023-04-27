using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using TaskMaster.Models.Entities;

namespace TaskMaster.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IRepository<TEntity, TKey> GetRequiredRepository<TEntity, TKey>() where TEntity : Entity<TKey>
        => new Repository<TEntity, TKey>(_dbContext);
    
    public async Task<int> CommitAsync() 
        => await _dbContext.SaveChangesAsync();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _dbContext.Dispose();
    }
}