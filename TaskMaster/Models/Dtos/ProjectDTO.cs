using NodaTime;
using TaskMaster.Models.Entities;
using TaskMaster.Models.Enums;

namespace TaskMaster.Models.Dtos;

public class ProjectDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public UserDTO Owner { get; set; }
    public LocalDateTime StartDate { get; set; }
    public LocalDateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Planning;
    public LocalDate CreatedAt { get; set; }
}