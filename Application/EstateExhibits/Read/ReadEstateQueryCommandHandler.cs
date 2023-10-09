using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.EstateExhibits.Read;

internal sealed class ReadEstateQueryCommandHandler : IRequestHandler<ReadEstateQuery, EstateResponse>
{
    private readonly IApplicationDbContext _context;

    public ReadEstateQueryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EstateResponse> Handle(ReadEstateQuery request, CancellationToken cancellationToken)
    {
        var estate = await _context
            .Estates
            .Where(e => e.Id == request.EstateId)
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
                    .Select(he => new HistoricalEventResponse(he.Id, he.Date.Date, he.Description, he.Impact))
                    .ToList()))
            .FirstOrDefaultAsync(cancellationToken);

        if(estate is null)
        {
            throw new Exception("Estate with given id does not exist in db!");
        }

        return estate;

    }
}
