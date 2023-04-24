using System.Runtime.Serialization;
using NodaTime;
using TaskMaster.Models.Dtos;
using TaskMaster.Models.Enums;

namespace TaskMaster.Controllers.Payloads.Requests;

[DataContract]
public class ProjectUpdatePatchRequest
{
    [DataMember]
    public FieldPatch<string>? Description { get; set; }
    [DataMember]
    public FieldPatch<DateTime>? StartDate { get; set; }
    [DataMember]
    public FieldPatch<DateTime>? EndDate { get; set; }
    [DataMember]
    public FieldPatch<ProjectStatus>? Status { get; set; }
}