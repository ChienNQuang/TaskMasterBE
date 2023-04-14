using System.ComponentModel.DataAnnotations;
using TaskMaster.Models.Enums;

namespace TaskMaster.Models.Entities;

public class ProjectEntity
{
    [Key] 
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public ProjectStatus Status { get; set; }
}