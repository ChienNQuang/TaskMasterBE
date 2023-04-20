using System.ComponentModel.DataAnnotations;
using NodaTime;
using TaskMaster.Models.Enums;

namespace TaskMaster.Models.Entities;

public class ProjectEntity : Entity<Guid>
{
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; }
    public UserEntity Owner { get; set; }
    public LocalDateTime StartDate { get; set; }
    public LocalDateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public LocalDate CreatedAt { get; set; }
}