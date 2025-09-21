
namespace ProjectManagementSystem.API.Extensions;

internal static class SwaggerExtension
{
    internal static void ConfigureSwagger(this IServiceCollection services)
    {

        services.AddSwaggerGen();
    }
}