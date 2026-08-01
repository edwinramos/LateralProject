using LateralProject.Application.DTOs;
using MediatR;

namespace LateralProject.Application.Features.LateralEntities.Queries.GetAll;

public sealed record GetAllLateralEntitiesQuery(
    string? Search,
    int Page = 1,
    int PageSize = 10)
    : IRequest<IReadOnlyList<LateralEntityDto>>;