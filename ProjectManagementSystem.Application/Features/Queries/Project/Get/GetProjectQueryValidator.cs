

using FluentValidation;

namespace ProjectManagementSystem.Application.Features.Queries.Project.Get;

public class GetProjectQueryValidator:AbstractValidator<GetProjectQuery>
{
    public GetProjectQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}
