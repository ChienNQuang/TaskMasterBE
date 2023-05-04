using FluentValidation;
using TaskMaster.Controllers.Payloads.Requests;

namespace TaskMaster.Validators;

public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    private UserUpdateRequestValidator()
    {
        RuleFor(u => u.Password)
            .NotNull()
            .WithMessage("Must provide password!")
            .MinimumLength(8)
            .WithMessage("Invalid password length!");
    }

    public static UserUpdateRequestValidator New() => new();
}