using FluentValidation;
using TaskMaster.Controllers.Payloads.Requests;

namespace TaskMaster.Validators;

public class UserValidator : AbstractValidator<UserCreateRequest>
{
    public UserValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(u => u.Email)
            // .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Must provide email address!")
            .EmailAddress()
            .WithMessage("Invalid email format!")
            .MaximumLength(320)
            .WithMessage("Invalid email length!");

        RuleFor(u => u.Username)
            // .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Must provide username!")
            .MaximumLength(100)
            .WithMessage("Invalid username length!");

        RuleFor(u => u.Password)
            .MinimumLength(8)
            .WithMessage("Invalid password length!");
    }
}