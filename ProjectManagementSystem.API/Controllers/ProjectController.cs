
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.Features.Commands.Project.Create;

namespace ProjectManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error.Message);
        }
        return Ok(result.Value);
    }
}
