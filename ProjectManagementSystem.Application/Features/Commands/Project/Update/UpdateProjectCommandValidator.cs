
using FluentValidation;
using ProjectManagementSystem.Domain.ValueObjects.Project;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Update;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
   .MaximumLength(ProjectName.MAX_LENGTH);

        RuleFor(x => x.CompanyNameForCostumer)
            .MaximumLength(ProjectCompanyNames.MAX_LENGTH);

        RuleFor(x => x.CompanyNameForExecutor)
            .MaximumLength(ProjectCompanyNames.MAX_LENGTH);
        RuleFor(x => x.Priority)
           .GreaterThanOrEqualTo(ProjectPriority.MinValue);

        RuleFor(x => x)
        .Must(x => !x.EndDate.HasValue || !x.StartDate.HasValue || x.EndDate >= x.StartDate);
        RuleFor(x => x)
           .Must(x => x.Name != null || x.CompanyNameForCostumer != null ||
                     x.CompanyNameForExecutor != null || x.Priority.HasValue ||
                     x.StartDate.HasValue || x.EndDate.HasValue);
    }
}
public class UpdUpdateProjectWithIdCommandValidator : AbstractValidator<UpdateProjectWithIdCommand>
{
    public UpdUpdateProjectWithIdCommandValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty()
          .NotEqual(Guid.Empty);
        RuleFor(x => x.Command)
           .NotEmpty()
           .SetValidator(new UpdateProjectCommandValidator());
    }
}
