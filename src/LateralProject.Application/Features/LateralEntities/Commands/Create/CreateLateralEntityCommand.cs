using MediatR;
using LateralProject.Application.DTOs;

namespace LateralProject.Application.Features.LateralEntities.Commands.Create;

public sealed record CreateLateralEntityCommand(
    string Description)
    : IRequest<LateralEntityDto>;