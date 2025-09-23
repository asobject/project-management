
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.API.Extensions;
using ProjectManagementSystem.Application.Features.Commands.Project.Create;

namespace ProjectManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToObjectResult();
    }
}