using Microsoft.EntityFrameworkCore;
using TaskMaster.Models.Entities;

namespace TaskMaster.Persistence;

public class TaskMasterContext : DbContext
{
    public TaskMasterContext(DbContextOptions<TaskMasterContext> options) : base(options)
    {
        
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
}