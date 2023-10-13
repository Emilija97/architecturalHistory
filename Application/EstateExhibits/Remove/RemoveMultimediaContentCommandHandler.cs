using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.EstateExhibits.Remove;

internal sealed class RemoveMultimediaContentCommandHandler : IRequestHandler<RemoveMultimediaContent>
{
    private readonly IApplicationDbContext _context;

    public RemoveMultimediaContentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveMultimediaContent request, CancellationToken cancellationToken)
    {
        var historicalEvent = await _context
            .HistoricalEvents
            .Include(he => he.MultimediaContents.Where(mc => mc.Id == request.MultimediaContentId))
            .SingleOrDefaultAsync(he => he.Id == request.HistoricalEventId, cancellationToken);

        if (historicalEvent is null)
        {
            return;
        }

        historicalEvent.RemoveMultimediaContent(request.MultimediaContentId);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
