using LateralProject.Infrastructure.Persistence;
using LateralProject.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LateralProject.Infrastructure;
using LateralProject.Domain.Repositories;
using System.Linq;

namespace LateralProject.IntegrationTests.Common;

public sealed class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    private readonly SqliteConnection _connection;

    public CustomWebApplicationFactory()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(DbContextOptions<LateralProjectDbContext>));
            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            var dbContextServiceDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(LateralProjectDbContext));
            if (dbContextServiceDescriptor != null)
            {
                services.Remove(dbContextServiceDescriptor);
            }

            var repositoryDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(ILateralEntityRepository));
            if (repositoryDescriptor != null)
            {
                services.Remove(repositoryDescriptor);
            }

            services.AddDbContext<LateralProjectDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            services.AddScoped<ILateralEntityRepository, LateralEntityRepository>();

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<LateralProjectDbContext>();
            context.Database.EnsureCreated();
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            _connection?.Dispose();
        }
    }
}