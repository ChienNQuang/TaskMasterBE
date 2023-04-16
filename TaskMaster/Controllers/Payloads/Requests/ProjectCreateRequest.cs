namespace TaskMaster.Controllers.Payloads.Requests;

public class ProjectCreateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}