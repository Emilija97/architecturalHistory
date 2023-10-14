using Application.DigitalTours.AddHighlight;
using Application.DigitalTours.AddInteractiveSession;
using Application.DigitalTours.AddVirtualTour;
using Application.DigitalTours.Create;
using Carter;
using Domain.Curators;
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

            app.MapPost("reservations/virtual_tour/add_highlight", async ([FromBody] AddHighlightToVirtualTourRequest request, ISender sender) =>
            {
                var command = new AddHighlightToVirtualTour(
                    new VirtualTourId(request.VirtualTourId),
                    request.Description);

                await sender.Send(command);

                return Results.Ok();
            });

            app.MapPost("reservations/virtual_tour/add_session", async ([FromBody] AddInteractiveSessionToVirtualTourRequest request, ISender sender) =>
            {
                var command = new AddInteractiveSessionToVirtualTour(
                    new VirtualTourId(request.VirtualTourId),
                    new ExpertId(request.ExpertId),
                    request.SheduledTime,
                    request.Duration);

                await sender.Send(command);

                return Results.Ok();
            });
        }
    }
}
