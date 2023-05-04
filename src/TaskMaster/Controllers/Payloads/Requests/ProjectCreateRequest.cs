using NodaTime;

namespace TaskMaster.Controllers.Payloads.Requests;

public class ProjectCreateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}