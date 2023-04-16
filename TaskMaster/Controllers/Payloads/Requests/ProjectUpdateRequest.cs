using TaskMaster.Models.Dtos;
using TaskMaster.Models.Enums;

namespace TaskMaster.Controllers.Payloads.Requests;

public class ProjectUpdateRequest
{
    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public ProjectStatus Status { get; set; }
}