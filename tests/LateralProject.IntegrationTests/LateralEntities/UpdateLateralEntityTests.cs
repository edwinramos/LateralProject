using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using LateralProject.Application.DTOs;
using LateralProject.Application.Features.LateralEntities.Commands.Create;
using LateralProject.Application.Features.LateralEntities.Commands.Update;
using LateralProject.IntegrationTests.Common;
using Xunit;

namespace LateralProject.IntegrationTests.LateralEntities;

public class UpdateLateralEntityTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UpdateLateralEntityTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Should_Update_LateralEntity()
    {
        var createCommand = new CreateLateralEntityCommand("Original Description");

        var createResponse = await _client.PostAsJsonAsync(
            "/api/lateralentities",
            createCommand);

        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdEntity = await createResponse.Content
            .ReadFromJsonAsync<LateralEntityDto>();

        createdEntity.Should().NotBeNull();

        var updateCommand = new UpdateLateralEntityCommand(
            createdEntity!.Id,
            "Updated Description");

        var updateResponse = await _client.PutAsJsonAsync(
            $"/api/lateralentities/{createdEntity.Id}",
            updateCommand);

        updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await _client.GetAsync(
            $"/api/lateralentities/{createdEntity.Id}");

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var updatedEntity = await getResponse.Content
            .ReadFromJsonAsync<LateralEntityDto>();

        updatedEntity.Should().NotBeNull();
        updatedEntity!.Id.Should().Be(createdEntity.Id);
        updatedEntity.Description.Should().Be("Updated Description");
    }
}