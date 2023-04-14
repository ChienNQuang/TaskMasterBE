using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskMaster.Models.Enums;
using TaskStatus = TaskMaster.Models.Enums.TaskStatus;

namespace TaskMaster.Models.Entities;

public class TaskEntity : Entity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly DueDate { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public TaskStatus Status { get; set; }
    public ProjectEntity Project { get; set; }
    public UserEntity User { get; set; }
}