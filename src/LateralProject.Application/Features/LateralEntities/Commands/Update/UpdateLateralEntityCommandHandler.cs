using LateralProject.Domain.Exceptions;
using LateralProject.Domain.Repositories;
using MediatR;

namespace LateralProject.Application.Features.LateralEntities.Commands.Update;

public sealed class UpdateLateralEntityCommandHandler
    : IRequestHandler<UpdateLateralEntityCommand>
{
    private readonly ILateralEntityRepository _repository;

    public UpdateLateralEntityCommandHandler(ILateralEntityRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        UpdateLateralEntityCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
            throw new DomainException("LateralEntity not found.");

        entity.Update(request.Description);

        await _repository.UpdateAsync(entity, cancellationToken);
    }
}