using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using LateralProject.Application.DTOs;
using LateralProject.Application.Features.LateralEntities.Commands.Create;
using LateralProject.IntegrationTests.Common;
using Xunit;

namespace LateralProject.IntegrationTests.LateralEntities;

public class DeleteLateralEntityTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DeleteLateralEntityTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Should_Delete_LateralEntity()
    {
        var createCommand = new CreateLateralEntityCommand("Entity To Delete");

        var createResponse = await _client.PostAsJsonAsync(
            "/api/lateralentities",
            createCommand);

        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdEntity = await createResponse.Content
            .ReadFromJsonAsync<LateralEntityDto>();

        createdEntity.Should().NotBeNull();

        var deleteResponse = await _client.DeleteAsync(
            $"/api/lateralentities/{createdEntity!.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await _client.GetAsync(
            $"/api/lateralentities/{createdEntity.Id}");

        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_Return_NotFound_When_Deleting_NonExisting_Entity()
    {
        var response = await _client.DeleteAsync(
            $"/api/lateralentities/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}