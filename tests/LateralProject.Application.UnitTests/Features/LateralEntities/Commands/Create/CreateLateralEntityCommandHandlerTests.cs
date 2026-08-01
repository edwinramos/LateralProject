using FluentAssertions;
using LateralProject.Application.Features.LateralEntities.Commands.Create;
using LateralProject.Domain.Entities;
using LateralProject.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System.Timers;
using Xunit;

namespace LateralProject.Application.UnitTests.Features.LateralEntities.Commands.Create;

public class CreateLateralEntityCommandHandlerTests
{
    private readonly Mock<ILateralEntityRepository> _repository = new();
    private readonly Mock<ILogger<CreateLateralEntityCommandHandler>> _logger = new();

    [Fact]
    public async Task Should_Create_New_Entity()
    {
        // Arrange
        _repository
            .Setup(x => x.DescriptionExistsAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _repository
            .Setup(x => x.AddAsync(
                It.IsAny<LateralEntity>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateLateralEntityCommandHandler(
            _repository.Object,
            _logger.Object);

        var command = new CreateLateralEntityCommand("Test");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Description.Should().Be("Test");

        _repository.Verify(x =>
            x.AddAsync(It.IsAny<LateralEntity>(),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_Throw_When_Description_Already_Exists()
    {
        _repository
            .Setup(x => x.DescriptionExistsAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new CreateLateralEntityCommandHandler(
            _repository.Object,
            _logger.Object);

        var command = new CreateLateralEntityCommand("Test");

        await FluentActions.Invoking(() =>
                handler.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<Exception>();
    }
}