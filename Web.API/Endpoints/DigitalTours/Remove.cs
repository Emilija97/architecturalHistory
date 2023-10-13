using Application.DigitalTours.Remove;
using Carter;
using Domain.DigitalTours;
using MediatR;

namespace Web.API.Endpoints.DigitalTours
{
    public class Remove : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("reservations/{id}/virtual_tour/{virtualTourId}", async (Guid id, Guid virtualTourId, ISender sender) =>
            {
                var command = new RemoveVirtualTour(new ReservationId(id), new VirtualTourId(virtualTourId));

                await sender.Send(command);

                return Results.Ok();
            });
        }
    }
}
