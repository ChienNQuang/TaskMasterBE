using System.Runtime.InteropServices.JavaScript;
using FluentValidation;
using TaskMaster.Models.Entities;

namespace TaskMaster.Validators;

public class ProjectValidator : AbstractValidator<ProjectEntity>
{
    public ProjectValidator()
    {
        RuleFor(p => p.StartDate)
            .LessThan(p => p.EndDate)
            .WithMessage("Start date must be less than end date.");
    }
}