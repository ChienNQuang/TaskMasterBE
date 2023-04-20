namespace TaskMaster.Controllers.Payloads.Requests;

public class ProjectCreateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}