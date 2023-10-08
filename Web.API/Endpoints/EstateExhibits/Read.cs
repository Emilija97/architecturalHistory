using Application.Curators.Read;
using Application.EstateExhibits.Read;
using Carter;
using Domain.EstateExhibits;
using MediatR;

namespace Web.API.Endpoints.EstateExhibits
{
    public class Read : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("estates/{id:guid}", async (Guid id, ISender sender) =>
            {
                try
                {
                    return Results.Ok(await sender.Send(new ReadEstateQuery(new EstateId(id))));
                }
                catch (Exception e)
                {
                    return Results.NotFound(e.Message);
                }
            });

            app.MapGet("estates", async (ISender sender) =>
                Results.Ok(await sender.Send(new GetAllEstatesQuery()))
            );
        }
    }
}
