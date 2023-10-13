using Application.Data;
using Domain.EstateExhibits;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.EstateExhibits.AddMultimediaContent;

internal sealed class AddMultimediaContentToHistoricalEventCommandHandler : IRequestHandler<AddMultimediaContentToHistoricalEvent>
{

    private readonly IApplicationDbContext _context;
    private readonly IPublisher _publisher;
    private readonly IRepository<HistoricalEvent, HistoricalEventId> _historicalEventsRepository;
    private readonly IRepository<MultimediaContent, MultimediaContentId> _multimediaContentsRepository;

    public AddMultimediaContentToHistoricalEventCommandHandler(
        IApplicationDbContext context,
        IPublisher publisher,
        IRepository<HistoricalEvent, HistoricalEventId> historicalEventsRepository,
        IRepository<MultimediaContent, MultimediaContentId> multimediaContentsRepository)
    {
        _context = context;
        _publisher = publisher;
        _historicalEventsRepository = historicalEventsRepository;
        _multimediaContentsRepository = multimediaContentsRepository;
    }

    public async Task Handle(AddMultimediaContentToHistoricalEvent request, CancellationToken cancellationToken)
    {

        var historicalEvents = await _context.HistoricalEvents.Include(he => he.MultimediaContents).ToListAsync(cancellationToken);

        var historicalEvent = await _historicalEventsRepository.GetByIdAsync(request.HistoricalEventId);

        if (historicalEvent is null)
        {
            return;
        }

        var addedContent = false;

        for(var i = 0; i < historicalEvent.MultimediaContents.Count; i++)
        {
            if (historicalEvent.MultimediaContents[i].Url == request.Url)
            {
                addedContent = true;
                break;
            }
        }

        if (!addedContent)
        {
            historicalEvent.AddMultimediaContent(request.Url, request.CreationDate.ToUniversalTime());
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Multimedia content with this url has been already added for this historical event!");
        }
    }
}
