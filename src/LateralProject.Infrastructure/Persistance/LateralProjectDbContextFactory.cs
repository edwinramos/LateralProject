using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LateralProject.Infrastructure.Persistence;

public sealed class LateralProjectDbContextFactory
    : IDesignTimeDbContextFactory<LateralProjectDbContext>
{
    public LateralProjectDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LateralProject.Api"))
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<LateralProjectDbContext>();

        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection"));

        return new LateralProjectDbContext(optionsBuilder.Options);
    }
}