using FluentValidation;
using ProjectManagementSystem.API.Extensions;
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


        services.AddScoped<IRepository<Project, Guid>, Repository<Project, Guid>>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));

        services.AddValidatorsFromAssembly(typeof(CreateProjectCommandValidator).Assembly);

        services.AddControllers();

        services.AddLogging();
    }
}
