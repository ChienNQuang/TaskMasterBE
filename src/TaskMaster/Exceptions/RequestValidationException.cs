using System.Runtime.Serialization;
using FluentValidation.Results;

namespace TaskMaster.Exceptions;

[Serializable]
public class RequestValidationException : Exception
{
    public List<ValidationFailure>? Errors { get; init; }

    public RequestValidationException(List<ValidationFailure>? errors) : base("User input validation failed!")
    {
        Errors = errors;
    }

    protected RequestValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Errors = (List<ValidationFailure>)info.GetValue(nameof(Errors), typeof(List<ValidationFailure>))!;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Errors), Errors);
        base.GetObjectData(info, context);
    }
}