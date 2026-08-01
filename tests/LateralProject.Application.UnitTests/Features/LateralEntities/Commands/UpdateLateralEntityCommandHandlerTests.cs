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

public class UpdateLateralEntityCommandHandlerTests
{
    private readonly Mock<ILateralEntityRepository> _repository = new();
    private readonly Mock<ILogger<CreateLateralEntityCommandHandler>> _logger = new();

    [Fact]
    public async Task Should_Update_Entity()
    {
        var entity = new LateralEntity("Old Description");

        _repository
            .Setup(x => x.GetByIdAsync(entity.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        _repository
            .Setup(x => x.UpdateAsync(entity, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateLateralEntityCommandHandler(
            _repository.Object);

        await handler.Handle(
            new UpdateLateralEntityCommand(entity.Id, "New Description"),
            CancellationToken.None);

        entity.Description.Should().Be("New Description");

        _repository.Verify(x =>
            x.UpdateAsync(entity, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_Throw_When_Updating_Non_Existing_Entity()
    {
        _repository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((LateralEntity?)null);

        var handler = new UpdateLateralEntityCommandHandler(
            _repository.Object);

        await FluentActions
            .Invoking(() =>
                handler.Handle(
                    new UpdateLateralEntityCommand(Guid.NewGuid(), "Test"),
                    CancellationToken.None))
            .Should()
            .ThrowAsync<DomainException>();
    }
}