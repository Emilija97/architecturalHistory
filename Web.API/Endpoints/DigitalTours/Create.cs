using Application.DigitalTours.AddVirtualTour;
using Application.DigitalTours.Create;
using Carter;
using Domain.DigitalTours;
using Domain.EstateExhibits;
using Domain.Participants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Endpoints.DigitalTours
{
    public class Create : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("reservations", async ([FromBody] CreateReservationRequest request , ISender sender) =>
            {
                var command = new CreateReservationCommand(
                    new ParticipantId(request.ParticipantId),
                    new EstateId(request.EstateId),
                    request.Currency,
                    request.Amount,
                    request.Duration,
                    request.NarattionLanguage,
                    request.OrganizedAt);

                await sender.Send(command);

                return Results.Ok();
            });

            app.MapPost("reservations/add_virtual_tour", async ([FromBody] AddVirtualTourToReservationRequest request, ISender sender) =>
            {
                var command = new AddVirtualTourToReservation(
                    new ReservationId(request.ReservationId),
                    new EstateId(request.EstateId),
                    request.Currency,
                    request.Amount,
                    request.Duration,
                    request.NarattionLanguage,
                    request.OrganizedAt);

                await sender.Send(command);

                return Results.Ok();
            });

            //app.MapDelete("bookings/{id}/tickets/{ticketId}", async (Guid id, Guid ticketId, ISender sender) =>
            //{
            //    var command = new RemoveTicket(new BookingId(id), new TicketId(ticketId));

            //    await sender.Send(command);

            //    return Results.Ok();
            //});
        }
    }
}
