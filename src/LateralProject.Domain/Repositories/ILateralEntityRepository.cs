using LateralProject.Domain.Entities;

namespace LateralProject.Domain.Repositories;

public interface ILateralEntityRepository
{
    Task<LateralEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<LateralEntity>> GetAllAsync(string? search, int page, int pageSize, CancellationToken cancellationToken = default);

    Task<bool> DescriptionExistsAsync(string description, CancellationToken cancellationToken = default);

    Task AddAsync(LateralEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(LateralEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(LateralEntity entity, CancellationToken cancellationToken = default);
}