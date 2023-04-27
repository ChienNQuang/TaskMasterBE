using FluentValidation;
using TaskMaster.Controllers.Payloads.Requests;

namespace TaskMaster.Validators;

public class ProjectCreateRequestValidator : AbstractValidator<ProjectCreateRequest>
{
    private ProjectCreateRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(p => p.StartDate)
            .NotNull()
            .WithMessage("Must provide starting date")
            .LessThan(p => p.EndDate)
            .WithMessage("Start date must be less than end date");
        RuleFor(p => p.EndDate)
            .NotNull()
            .WithMessage("Must provide ending date")
            .NotEmpty()
            .WithMessage("End date must not be empty!");
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Must provide project name")
            .MaximumLength(100)
            .WithMessage("Name too long lol");
        RuleFor(p => p.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Must provide project description")
            .MaximumLength(1000)
            .WithMessage("Description too long");
    }

    public static ProjectCreateRequestValidator New() => new();
}