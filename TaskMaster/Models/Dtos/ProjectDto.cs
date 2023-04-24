using NodaTime;
using TaskMaster.Models.Entities;
using TaskMaster.Models.Enums;

namespace TaskMaster.Models.Dtos;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public UserDto Owner { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public DateOnly CreationDate { get; set; }
}