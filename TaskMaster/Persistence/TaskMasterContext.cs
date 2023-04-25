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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CreateUserEntityModel(modelBuilder);
    }

    private void CreateUserEntityModel(ModelBuilder modelBuilder)
    {
        var userEntity = modelBuilder.Entity<UserEntity>();
        userEntity.HasKey(u => u.Id);
        userEntity.HasIndex(u => u.Email)
            .IsUnique();
        userEntity.HasIndex(u => u.Username)
            .IsUnique();
        userEntity.Property(u => u.Email).HasMaxLength(320).IsRequired();
        userEntity.Property(u => u.Username).HasMaxLength(100).IsRequired();
        userEntity.Property(u => u.HashedPassword).HasMaxLength(256).IsRequired();
        userEntity.Property(u => u.Active).IsRequired();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
}