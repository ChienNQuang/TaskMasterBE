using System.ComponentModel.DataAnnotations;
using NodaTime;
using TaskMaster.Models.Enums;
using TaskStatus = TaskMaster.Models.Enums.TaskStatus;

namespace TaskMaster.Models.Entities;

public class TaskEntity : Entity<Guid>
{
    [MaxLength(50)]
    public string Title { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; }
    public LocalDateTime DueDate { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public TaskLabel Tag { get; set; }
    public TaskStatus Status { get; set; }
    public ProjectEntity Project { get; set; }
    public UserEntity User { get; set; }
}