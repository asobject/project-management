
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.API.Extensions;
using ProjectManagementSystem.Application.Features.Commands.Project.Create;
using ProjectManagementSystem.Application.Features.Commands.Project.Delete;
using ProjectManagementSystem.Application.Features.Commands.Project.Update;
using ProjectManagementSystem.Application.Features.Queries.Project.Get;

namespace ProjectManagementSystem.API.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult(201, created => $"/api/project/{created.Id}");
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProject([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProjectQuery(id), cancellationToken);
        return result.ToActionResult();
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProject([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteProjectCommand(id), cancellationToken);
        return result.ToActionResult(204);
    }
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateProject([FromRoute] Guid id, [FromBody] UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateProjectWithIdCommand(id, command), cancellationToken);
        return result.ToActionResult(204);
    }
}