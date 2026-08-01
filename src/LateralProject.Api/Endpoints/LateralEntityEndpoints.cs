using LateralProject.Application.Features.LateralEntities.Commands.Create;
using LateralProject.Application.Features.LateralEntities.Commands.Delete;
using LateralProject.Application.Features.LateralEntities.Commands.Update;
using LateralProject.Application.Features.LateralEntities.Queries.GetAll;
using LateralProject.Application.Features.LateralEntities.Queries.GetById;
using MediatR;

namespace LateralProject.Api.Endpoints;

public static class LateralEntityEndpoints
{
    public static IEndpointRouteBuilder MapLateralEntityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/lateralentities",
            async (
                string? search,
                int page,
                int pageSize,
                ISender sender) =>
            {
                var result = await sender.Send(
                    new GetAllLateralEntitiesQuery(
                        search,
                        page == 0 ? 1 : page,
                        pageSize == 0 ? 10 : pageSize));

                return Results.Ok(result);
            });

        app.MapGet("/api/lateralentities/{id:guid}",
            async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetLateralEntityByIdQuery(id));

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

        app.MapPost("/api/lateralentities",
            async (
                CreateLateralEntityCommand command,
                ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Created($"/api/lateralentities/{result.Id}", result);
            });

        app.MapPut("/api/lateralentities/{id:guid}",
            async (
                Guid id,
                UpdateLateralEntityCommand command,
                ISender sender) =>
            {
                await sender.Send(command with { Id = id });

                return Results.NoContent();
            });

        app.MapDelete("/api/lateralentities/{id:guid}",
            async (Guid id, ISender sender) =>
            {
                await sender.Send(new DeleteLateralEntityCommand(id));

                return Results.NoContent();
            });

        return app;
    }
}