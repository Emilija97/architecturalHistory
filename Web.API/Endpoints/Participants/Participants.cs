using Application.Participants.Create;
using Application.Participants.Read;
using Application.Participants.Update;
using Carter;
using Domain.Participants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Endpoints.Participants;

public class Participants : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("participants", async (CreateParticipantCommand command, ISender sender) =>
        {
            await sender.Send(command);

            return Results.Ok();
        });

        app.MapGet("participants", async (ISender sender) =>
            Results.Ok(await sender.Send(new GetAllParticipantsQuery()))
        );

        app.MapGet("participants/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new ReadParticipantQuery(new ParticipantId(id))));
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapPut("participants/{id:guid}", async (Guid id, [FromBody] UpdateParticipantRequest request, ISender sender) =>
        {
            var command = new UpdateParticipantCommand(
                new ParticipantId(id),
                request.FirstName,
                request.LastName);

            await sender.Send(command);

            return Results.NoContent();
        });
    }
}
