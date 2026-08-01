using LateralProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LateralProject.Infrastructure.Persistence;

public sealed class LateralProjectDbContext : DbContext
{
    public LateralProjectDbContext(DbContextOptions<LateralProjectDbContext> options)
        : base(options)
    {
    }

    public DbSet<LateralEntity> LateralEntities => Set<LateralEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LateralProjectDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}