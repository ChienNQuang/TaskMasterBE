using System.Runtime.Serialization;

namespace TaskMaster.Controllers.Payloads.Requests;

[DataContract]
public class FieldPatch<T>
{
    [DataMember(Name = "value")]
    public T? Value { get; set; }
}