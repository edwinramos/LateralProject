using LateralProject.Application.DTOs;
using LateralProject.Domain.Entities;
using LateralProject.Domain.Exceptions;
using LateralProject.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LateralProject.Application.Features.LateralEntities.Commands.Create;

public sealed class CreateLateralEntityCommandHandler
    : IRequestHandler<CreateLateralEntityCommand, LateralEntityDto>
{
    private readonly ILateralEntityRepository _repository;
    private readonly ILogger<CreateLateralEntityCommandHandler> _logger;

    public CreateLateralEntityCommandHandler(
        ILateralEntityRepository repository,
        ILogger<CreateLateralEntityCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<LateralEntityDto> Handle(
        CreateLateralEntityCommand request,
        CancellationToken cancellationToken)
    {
        if (await _repository.DescriptionExistsAsync(request.Description, cancellationToken))
        {
            throw new DomainException("A LateralEntity with the same description already exists.");
        }

        var entity = new LateralEntity(request.Description);

        await _repository.AddAsync(entity, cancellationToken);
        
        _logger.LogInformation("LateralEntity {Id} created.", entity.Id);

        return new LateralEntityDto(
            entity.Id,
            entity.Description,
            entity.CreatedDateTime,
            entity.ModifiedDateTime);
    }
}