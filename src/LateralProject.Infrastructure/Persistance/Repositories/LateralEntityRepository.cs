using LateralProject.Domain.Entities;
using LateralProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LateralProject.Infrastructure.Persistence.Repositories;

public sealed class LateralEntityRepository : ILateralEntityRepository
{
    private readonly LateralProjectDbContext _context;

    public LateralEntityRepository(LateralProjectDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LateralEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.LateralEntities.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<LateralEntity>> GetAllAsync(string? search, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.LateralEntities.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.Description.Contains(search));
        }

        return await query
            .OrderBy(x => x.Description)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(LateralEntity entity, CancellationToken cancellationToken = default)
    {
        _context.LateralEntities.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DescriptionExistsAsync(string description, CancellationToken cancellationToken = default)
    {
        return await _context.LateralEntities
            .AnyAsync(x => x.Description == description, cancellationToken);
    }

    public async Task<IReadOnlyList<LateralEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.LateralEntities.ToListAsync(cancellationToken);
    }

    public async Task<LateralEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.LateralEntities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(LateralEntity entity, CancellationToken cancellationToken = default)
    {
        _context.LateralEntities.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}