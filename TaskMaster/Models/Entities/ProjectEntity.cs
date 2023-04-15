using System.ComponentModel.DataAnnotations;
using TaskMaster.Models.Enums;

namespace TaskMaster.Models.Entities;

public class ProjectEntity : Entity<Guid>
{
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; }

    public UserEntity Owner { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public DateOnly CreatedAt { get; set; }
}