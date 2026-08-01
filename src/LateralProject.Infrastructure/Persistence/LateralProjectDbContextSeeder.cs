using Bogus;
using LateralProject.Domain.Entities;

namespace LateralProject.Infrastructure.Persistence;

public static class LateralProjectDbContextSeeder
{
    public static async Task SeedAsync(LateralProjectDbContext context)
    {
        if (context.LateralEntities.Any())
            return;

        var faker = new Faker<LateralEntity>()
            .CustomInstantiator(f =>
                new LateralEntity(f.Commerce.ProductDescription()));

        var entities = Enumerable.Range(1, 100)
    .Select(i => new LateralEntity($"Lateral Entity {i}"))
    .ToList();

        await context.LateralEntities.AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }
}