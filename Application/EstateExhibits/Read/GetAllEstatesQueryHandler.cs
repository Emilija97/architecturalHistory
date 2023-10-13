using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.EstateExhibits.Read;

internal sealed class GetAllEstatesQueryHandler : IRequestHandler<GetAllEstatesQuery, List<EstateResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllEstatesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EstateResponse>> Handle(GetAllEstatesQuery request, CancellationToken cancellationToken)
    {
        var estates = await _context
            .Estates
            .Select(e => new EstateResponse(
                e.Id.Value,
                e.Name,
                e.ArchitecturalStyle.Name,
                e.ArchitecturalStyle.Period,
                e.ArchitecturalStyle.Description,
                e.Location.Latitude,
                e.Location.Longitude,
                e.Location.Address,
                e.YearBuilt.Year,
                e.Events
                    .Select(he => new HistoricalEventResponse(he.Id.Value, he.Date.Date, he.Description, he.Impact,
                he.MultimediaContents
                    .Select(mc => new MultimediaContentResponse(mc.Id.Value, mc.Url, mc.CreationDate.Date))
                    .ToList()))
                    .ToList()))
            .ToListAsync(cancellationToken);

        return estates is null ? throw new Exception("There is no any estate yet!") : estates;
    }
}