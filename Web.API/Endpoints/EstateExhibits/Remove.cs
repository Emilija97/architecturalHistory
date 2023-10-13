using Application.EstateExhibits.Remove;
using Carter;
using Domain.EstateExhibits;
using MediatR;

namespace Web.API.Endpoints.EstateExhibits
{
    public class Remove : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("estates/historical_event/{he_id}/content/{contentId}", async (Guid he_id, Guid contentId, ISender sender) =>
            {
                var command = new RemoveMultimediaContent(new HistoricalEventId(he_id), new MultimediaContentId(contentId));

                await sender.Send(command);

                return Results.Ok();
            });
        }
    }
}
