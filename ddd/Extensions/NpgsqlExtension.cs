using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Infrastructure.Data;

namespace ProjectManagementSystem.API.Extensions;

internal static class NpgsqlExtension
{
    internal static void ConfigureContextNpgsql(this IServiceCollection services, ConfigurationManager configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
       
    }
}