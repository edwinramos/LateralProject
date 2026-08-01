using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using LateralProject.Application.Features.LateralEntities.Commands.Create;
using LateralProject.IntegrationTests.Common;
using Xunit;

namespace LateralProject.IntegrationTests.LateralEntities;

public class CreateLateralEntityTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CreateLateralEntityTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Should_Create_LateralEntity()
    {
        var command = new CreateLateralEntityCommand("Integration Test");

        var response = await _client.PostAsJsonAsync(
            "/api/lateralentities",
            command);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_Description_Is_Empty()
    {
        var command = new CreateLateralEntityCommand(string.Empty);

        var response = await _client.PostAsJsonAsync(
            "/api/lateralentities",
            command);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}