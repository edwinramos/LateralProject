using MediatR;

namespace LateralProject.Application.Features.LateralEntities.Commands.Delete;

public sealed record DeleteLateralEntityCommand(Guid Id) : IRequest;