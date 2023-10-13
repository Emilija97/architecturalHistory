using Application.Data;
using Domain.EstateExhibits;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Application.EstateExhibits.Read;

internal class GetAllHistoricalEventsQueryHandler : IRequestHandler<GetAllHistoricalEventsQuery, List<HistoricalEventResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IRepository<HistoricalEvent, HistoricalEventId> _historicalEventRepository;

    public GetAllHistoricalEventsQueryHandler(IApplicationDbContext context, IRepository<HistoricalEvent, HistoricalEventId> historicalEventRepository)
    {
        _context = context;
        _historicalEventRepository = historicalEventRepository;
    }

    public async Task<List<HistoricalEventResponse>> Handle(GetAllHistoricalEventsQuery request, CancellationToken cancellationToken)
    {
        var historicalEvents = await _context
            .HistoricalEvents
            .Where(he => he.EstateId == request.EstateId)
            .Select(he => new HistoricalEventResponse(
                he.Id.Value,
                he.Date.Date,
                he.Description,
                he.Impact,
                he.MultimediaContents
                    .Select(mc => new MultimediaContentResponse(mc.Id.Value, mc.Url, mc.CreationDate.Date))
                    .ToList()))
            .ToListAsync(cancellationToken);

        return historicalEvents is null ? throw new Exception("There is no any historical event yet for given estate id!") : historicalEvents;
    }
}
 