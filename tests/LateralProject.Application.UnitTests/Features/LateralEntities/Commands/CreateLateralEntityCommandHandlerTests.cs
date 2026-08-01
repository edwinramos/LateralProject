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

public class CreateLateralEntityCommandHandlerTests
{
    private readonly Mock<ILateralEntityRepository> _repository = new();
    private readonly Mock<ILogger<CreateLateralEntityCommandHandler>> _logger = new();

    [Fact]
    public async Task Should_Create_New_Entity()
    {
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

        var result = await handler.Handle(command, CancellationToken.None);

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

    [Fact]
    public void Should_Have_Error_When_Description_Is_Empty()
    {
        var validator = new CreateLateralEntityCommandValidator();

        var result = validator.Validate(
            new CreateLateralEntityCommand(string.Empty));

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Update_ModifiedDate_When_Updating()
    {
        var entity = new LateralEntity("Old");

        var created = entity.ModifiedDateTime;

        Thread.Sleep(5);

        entity.Update("New");

        entity.ModifiedDateTime.Should().BeAfter(created);
    }
}