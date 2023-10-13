using Application.DigitalTours.Read;
using Application.EstateExhibits.Read;
using Carter;
using Domain.Participants;
using MediatR;

namespace Web.API.Endpoints.DigitalTours;

public class Read : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("reservations/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new GetReservationQuery(id)));
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapGet("reservations", async (ISender sender) =>
                Results.Ok(await sender.Send(new GetAllReservations()))
        );

        app.MapGet("reservations/participant/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new GetAllReservationsForParticipant(new ParticipantId(id))));
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });
    }
}
