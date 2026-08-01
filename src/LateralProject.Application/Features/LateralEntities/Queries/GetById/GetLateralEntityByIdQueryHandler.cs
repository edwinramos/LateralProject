using LateralProject.Application.DTOs;
using LateralProject.Domain.Repositories;
using MediatR;

namespace LateralProject.Application.Features.LateralEntities.Queries.GetById;

public sealed class GetLateralEntityByIdQueryHandler
    : IRequestHandler<GetLateralEntityByIdQuery, LateralEntityDto?>
{
    private readonly ILateralEntityRepository _repository;

    public GetLateralEntityByIdQueryHandler(ILateralEntityRepository repository)
    {
        _repository = repository;
    }

    public async Task<LateralEntityDto?> Handle(
        GetLateralEntityByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
            return null;

        return new LateralEntityDto(
            entity.Id,
            entity.Description,
            entity.CreatedDateTime,
            entity.ModifiedDateTime);
    }
}