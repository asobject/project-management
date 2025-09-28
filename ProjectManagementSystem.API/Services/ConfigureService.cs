using FluentValidation;
using MediatR;
using ProjectManagementSystem.API.Behaviors;
using ProjectManagementSystem.API.Extensions;
using ProjectManagementSystem.API.Middlewares;
using ProjectManagementSystem.Application.Features.Commands.Project.Create;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Infrastructure.Repository;
using Shared.Contracts.Repository;

namespace ProjectManagementSystem.API.Services;

public static class ConfigureService
{
    public static void ConfigureHosting(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.ConfigureContextNpgsql(configuration);
        services.ConfigureSwagger();

        services.AddHttpContextAccessor();
        services.AddScoped<IRepository<Project, Guid>, Repository<Project, Guid>>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(CreateProjectCommandValidator).Assembly);

        services.AddControllers();

        services.AddLogging();
        services.AddTransient<GlobalExceptionMiddleware>();
    }
}
