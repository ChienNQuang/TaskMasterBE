using FluentValidation.Results;

namespace TaskMaster.Exceptions;

public class UserValidationException : Exception
{
    public List<ValidationFailure> Errors { get; init; }

    public UserValidationException(List<ValidationFailure> errors) : base("User input validated failed!")
    {
        Errors = errors;
    }
}