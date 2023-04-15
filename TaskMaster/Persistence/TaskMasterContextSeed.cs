using System.Runtime.CompilerServices;
using TaskMaster.Models.Entities;
using TaskMaster.Models.Enums;
using ILogger = Serilog.ILogger;
using TaskStatus = TaskMaster.Models.Enums.TaskStatus;

namespace TaskMaster.Persistence;

public class TaskMasterContextSeed
{
    public static async Task SeedUserAsync(TaskMasterContext context, ILogger logger)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(getUsers());
            await context.SaveChangesAsync();
            logger.Information($"Seeded data for TaskMaster DB associated with context {nameof(context)}");
        }
    }
    public static async Task SeedProjectAsync(TaskMasterContext context, ILogger logger)
    {
        if (!context.Projects.Any())
        {
            context.Projects.AddRange(getProjects());
            await context.SaveChangesAsync();
            logger.Information($"Seeded data for TaskMaster DB associated with context {nameof(context)}");
        }
    }
    public static async Task SeedTaskAsync(TaskMasterContext context, ILogger logger)
    {
        if (!context.Tasks.Any())
        {
            context.Tasks.AddRange(getTasks());
            await context.SaveChangesAsync();
            logger.Information($"Seeded data for TaskMaster DB associated with context {nameof(context)}");
        }
    }
    
    private static IEnumerable<UserEntity> getUsers() => new List<UserEntity>()
    {
        new()
        {
            Id = new Guid("EE2D8702-0F9B-46E8-82E4-9AABFAE7824F"),
            Email = "quangchieens1@gmail.com",
            HashedPassword = "Something something",
            Username = "ezarp",
        },
        new()
        {
            Id = new Guid("B0A13125-2DFE-4955-9D7E-DF96E603A7D9"),
            Email = "quangcheems@gmail.com",
            HashedPassword = "Something else",
            Username = "ezarp1",
        }
    };
    
    private static IEnumerable<ProjectEntity> getProjects() => new List<ProjectEntity>()
    {
        new()
        {
            Id = new Guid("5C4D611A-0BD8-4FEE-87D9-AA54D8084F49"),
            Name = "Haha",
            Description = "Hehe",
            Status = ProjectStatus.Planning,
            StartDate = new DateOnly(2022, 1,1 ),
            EndDate = new DateOnly(2024, 1, 1)
        },
        new()
        {
            Id = new Guid("A0D340E7-8C55-4DAE-947F-21C45E9F8C94"),
            Name = "Chichi",
            Description = "Chanhchanh",
            Status = ProjectStatus.Delayed,
            StartDate = new DateOnly(2022, 1,1 ),
            EndDate = new DateOnly(2024, 1, 1)
        }
    };

    private static IEnumerable<TaskEntity> getTasks() => new List<TaskEntity>()
    {
        new()
        {
            Id = new Guid("F57FDC35-1B83-48CD-8479-A0C6FAB9D937"),
            Title = "Get this done",
            Description = "A very important task",
            DueDate = new DateOnly(2023, 4, 20),
            PriorityLevel = PriorityLevel.Normal,
            Status = TaskStatus.InProgress,
            User = getUsers().ElementAt(0),
            Project = getProjects().ElementAt(1),
            Tag = TaskLabel.Bug
        },
        new()
        {
            Id = new Guid("F97A5151-5466-443F-82D2-9FE1689A2A24"),
            Title = "This is very bad",
            Description = "Hurry up!!",
            DueDate = new DateOnly(2023, 4, 16),
            PriorityLevel = PriorityLevel.Critical,
            Status = TaskStatus.OnHold,
            User = getUsers().ElementAt(1),
            Project = getProjects().ElementAt(1),
            Tag = TaskLabel.Design
        }
    };
}