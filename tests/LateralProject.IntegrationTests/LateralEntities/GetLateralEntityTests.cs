using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using LateralProject.Application.DTOs;
using LateralProject.Application.Features.LateralEntities.Commands.Create;
using LateralProject.IntegrationTests.Common;
using Xunit;

namespace LateralProject.IntegrationTests.LateralEntities;

public class GetLateralEntityTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetLateralEntityTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Should_Create_And_Retrieve_LateralEntity()
    {
        var command = new CreateLateralEntityCommand("Integration Test Entity");

        var createResponse = await _client.PostAsJsonAsync(
            "/api/lateralentities",
            command);

        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdEntity = await createResponse.Content
            .ReadFromJsonAsync<LateralEntityDto>();

        createdEntity.Should().NotBeNull();

        var getResponse = await _client.GetAsync(
            $"/api/lateralentities/{createdEntity!.Id}");

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var entity = await getResponse.Content
            .ReadFromJsonAsync<LateralEntityDto>();

        entity.Should().NotBeNull();
        entity!.Id.Should().Be(createdEntity.Id);
        entity.Description.Should().Be("Integration Test Entity");
    }
}