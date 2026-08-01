using LateralProject.Domain.Repositories;
using LateralProject.Infrastructure.Persistence;
using LateralProject.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LateralProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<DbContextOptionsBuilder>? configureDb = null)
    {
        services.AddDbContext<LateralProjectDbContext>(options =>
        {
            if (configureDb is not null)
            {
                configureDb(options);
            }
            else
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"));
            }
        });

        services.AddScoped<ILateralEntityRepository, LateralEntityRepository>();

        return services;
    }
}