using System.Globalization;
using FluentValidation;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Models.Entities;
using TaskMaster.Repositories;

namespace TaskMaster.Validators;

public class ProjectUpdatePatchRequestValidator : AbstractValidator<ProjectUpdatePatchRequest>
{
    private readonly IRepository<ProjectEntity, Guid> _projectRepository;
    private readonly Guid _projectId;
    private ProjectUpdatePatchRequestValidator(IRepository<ProjectEntity, Guid> projectRepository, Guid projectId)
    {
        _projectRepository = projectRepository;
        _projectId = projectId;
        RuleFor(p => StartDateAndEndDate(p))
            .Custom(DateValidate);
    }

    private static (FieldPatch<DateTime>? StartDate, FieldPatch<DateTime>? EndDate)
        StartDateAndEndDate(ProjectUpdatePatchRequest request) => (request.StartDate, request.EndDate);

    private void DateValidate((FieldPatch<DateTime>? StartDate, FieldPatch<DateTime>? EndDate) dates,
        ValidationContext<ProjectUpdatePatchRequest> context)
    {
        const string errorMessage = "Start date must be less than end date!";
        if (dates.StartDate is null && dates.EndDate is null) return;
        
        if (dates.StartDate is not null && dates.EndDate is not null)
        {
            if (dates.StartDate.Value >= dates.EndDate.Value)
            {
                context.AddFailure(nameof(dates.StartDate), errorMessage);
            }
            return;
        }
        
        var project = _projectRepository.GetByIdAsync(_projectId).GetAwaiter().GetResult();
        if (dates.StartDate is not null && dates.StartDate.Value >= project!.EndDate.ToDateTimeUnspecified())
        {
            context.AddFailure(nameof(dates.StartDate), errorMessage);
        }
    
        if (dates.EndDate is not null && dates.EndDate.Value < project!.StartDate.ToDateTimeUnspecified())
        {
            context.AddFailure(nameof(dates.EndDate), errorMessage);
        }
        
    }
    public static ProjectUpdatePatchRequestValidator New(IRepository<ProjectEntity, Guid> repo, Guid id) => new(repo, id);
}