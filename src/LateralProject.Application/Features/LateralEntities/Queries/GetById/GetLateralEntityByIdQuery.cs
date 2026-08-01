using LateralProject.Application.DTOs;
using MediatR;

namespace LateralProject.Application.Features.LateralEntities.Queries.GetById;

public sealed record GetLateralEntityByIdQuery(Guid Id)
    : IRequest<LateralEntityDto?>;