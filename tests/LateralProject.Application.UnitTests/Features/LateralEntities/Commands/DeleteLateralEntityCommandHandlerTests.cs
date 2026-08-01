using FluentAssertions;
using LateralProject.Application.Features.LateralEntities.Commands.Create;
using LateralProject.Application.Features.LateralEntities.Commands.Delete;
using LateralProject.Application.Features.LateralEntities.Commands.Update;
using LateralProject.Domain.Entities;
using LateralProject.Domain.Exceptions;
using LateralProject.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System.Timers;
using Xunit;

namespace LateralProject.Application.UnitTests.Features.LateralEntities.Commands;

public class DeleteLateralEntityCommandHandlerTests
{
    private readonly Mock<ILateralEntityRepository> _repository = new();
    private readonly Mock<ILogger<CreateLateralEntityCommandHandler>> _logger = new();

    [Fact]
    public async Task Should_Delete_Entity()
    {
        var entity = new LateralEntity("Test");

        _repository
            .Setup(x => x.GetByIdAsync(entity.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        _repository
            .Setup(x => x.DeleteAsync(entity, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteLateralEntityCommandHandler(
            _repository.Object, _logger.Object);

        await handler.Handle(
            new DeleteLateralEntityCommand(entity.Id),
            CancellationToken.None);

        _repository.Verify(x =>
            x.DeleteAsync(entity, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_Throw_When_Deleting_Non_Existing_Entity()
    {
        _repository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((LateralEntity?)null);

        var handler = new DeleteLateralEntityCommandHandler(
            _repository.Object, _logger.Object);

        await FluentActions
            .Invoking(() =>
                handler.Handle(
                    new DeleteLateralEntityCommand(Guid.NewGuid()),
                    CancellationToken.None))
            .Should()
            .ThrowAsync<DomainException>();
    }
}