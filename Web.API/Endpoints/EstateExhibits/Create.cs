using Application.EstateExhibits.AddMultimediaContent;
using Application.EstateExhibits.Create;
using Carter;
using Domain.EstateExhibits;
using MediatR;

namespace Web.API.Endpoints.EstateExhibits
{
    public class Create : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("estates", async (CreateEstateCommand command , ISender sender) =>
            {
                await sender.Send(command);

                return Results.Ok();
            });

            app.MapPost("estates/add_historical_event", async (Guid estateId, DateTime date, string description, string impact, ISender sender) =>
            {
                var command = new AddHistoricalEventToEstate(new EstateId(estateId), date, description, impact);

                await sender.Send(command);

                return Results.Ok();
            });

            app.MapDelete("estates/historical_event/add_multimedia_content", async (Guid historicalEventId, string url, DateTime creationDate, ISender sender) =>
            {
                var command = new AddMultimediaContentToHistoricalEvent(new HistoricalEventId(historicalEventId), url, creationDate);

                await sender.Send(command);

                return Results.Ok();
            });
        }
    }
}
