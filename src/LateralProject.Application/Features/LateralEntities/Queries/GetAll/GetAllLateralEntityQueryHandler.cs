using LateralProject.Application.DTOs;
using LateralProject.Domain.Repositories;
using MediatR;

namespace LateralProject.Application.Features.LateralEntities.Queries.GetAll;

public sealed class GetAllLateralEntitiesQueryHandler
    : IRequestHandler<GetAllLateralEntitiesQuery, IReadOnlyList<LateralEntityDto>>
{
    private readonly ILateralEntityRepository _repository;

    public GetAllLateralEntitiesQueryHandler(ILateralEntityRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<LateralEntityDto>> Handle(
        GetAllLateralEntitiesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(
            request.Search,
            request.Page,
            request.PageSize,
            cancellationToken);

        return entities
            .Select(x => new LateralEntityDto(
                x.Id,
                x.Description,
                x.CreatedDateTime,
                x.ModifiedDateTime))
            .ToList();
    }
}