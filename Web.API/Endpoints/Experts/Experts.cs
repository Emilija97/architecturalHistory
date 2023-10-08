using Application.Curators.AddEstate;
using Application.Curators.Create;
using Application.Curators.Read;
using Application.Curators.RemoveEstate;
using Application.Curators.Update;
using Carter;
using Domain.Curators;
using Domain.EstateExhibits;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Endpoints.Experts;

public class Experts : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("experts", async (CreateExpertCommand command, ISender sender) =>
        {
            await sender.Send(command);

            return Results.Ok();
        });

        app.MapPost("experts/associate-estate", async (Guid expertId, Guid estateId, ISender sender) =>
        {
            var command = new AssociateEstateToExpert(new ExpertId(expertId), new EstateId(estateId));

            await sender.Send(command);

            return Results.Ok();
        });

        app.MapGet("experts", async (ISender sender) =>
            Results.Ok(await sender.Send(new GetAllExpertsQuery()))
        );

        app.MapGet("experts/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new ReadExpertQuery(new ExpertId(id))));
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapPut("experts/{id:guid}", async (Guid id, [FromBody] UpdateExpertRequest request, ISender sender) =>
        {
            var command = new UpdateExpertCommand(
                new ExpertId(id),
                request.Name,
                request.Biography);

            await sender.Send(command);

            return Results.NoContent();
        });

        app.MapDelete("expert/{id}/estates/{estateId}", async (Guid id, Guid estateId, ISender sender) =>
        {
            var command = new RemoveAssociatedEstate(new ExpertId(id), new EstateId(estateId));

            await sender.Send(command);

            return Results.Ok();
        });
    }
}
