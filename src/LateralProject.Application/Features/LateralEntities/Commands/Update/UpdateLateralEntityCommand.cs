using MediatR;

namespace LateralProject.Application.Features.LateralEntities.Commands.Update;

public sealed record UpdateLateralEntityCommand(
    Guid Id,
    string Description) : IRequest;