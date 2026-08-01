using LateralProject.Application.Features.LateralEntities.Commands.Create;
using LateralProject.Domain.Exceptions;
using LateralProject.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LateralProject.Application.Features.LateralEntities.Commands.Delete;

public sealed class DeleteLateralEntityCommandHandler
    : IRequestHandler<DeleteLateralEntityCommand>
{
    private readonly ILateralEntityRepository _repository;
    private readonly ILogger<CreateLateralEntityCommandHandler> _logger;

    public DeleteLateralEntityCommandHandler(ILateralEntityRepository repository, ILogger<CreateLateralEntityCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(
        DeleteLateralEntityCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
            throw new DomainException("LateralEntity not found.");

        _logger.LogInformation("LateralEntity {Id} created.", entity.Id);

        await _repository.DeleteAsync(entity, cancellationToken);
    }
}