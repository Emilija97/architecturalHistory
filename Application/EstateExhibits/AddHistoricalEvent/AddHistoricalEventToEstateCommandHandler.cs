using Application.Data;
using Application.EstateExhibits.Create;
using Domain.EstateExhibits;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.EstateExhibits.AddMultimediaContent;

internal sealed class AddHistoricalEventToEstateCommandHandler : IRequestHandler<AddHistoricalEventToEstate>
{

    private readonly IApplicationDbContext _context;
    private readonly IPublisher _publisher;
    private readonly IRepository<Estate> _estateRepository;

    public AddHistoricalEventToEstateCommandHandler(
        IApplicationDbContext context,
        IPublisher publisher,
        IRepository<Estate> estateRepository)
    {
        _context = context;
        _publisher = publisher;
        _estateRepository = estateRepository;
    }

    public async Task Handle(AddHistoricalEventToEstate request, CancellationToken cancellationToken)
    {

        var estates = await _context.Estates.Include(e => e.Events).ToListAsync(cancellationToken);

        var estate = await _estateRepository.GetByIdAsync(request.EstateId.Value);


        if (estate is null)
        {
            return;
        }

        var addedEvent = false;

        for (var i = 0; i < estate.Events.Count; i++)
        {
            if (estate.Events[i].Date == request.Date)
            {
                addedEvent = true;
                break;
            }
        }

        if (!addedEvent)
        {
            estate.AddHistoricalEvent(request.Date, request.Description, request.Impact);
            await _context.SaveChangesAsync();
            await _publisher.Publish(new EstateCreatedEvent(estate.Id), cancellationToken);
        }
        else
        {
            await _publisher.Publish(new EstateRejectedEvent(estate.Id), cancellationToken);
            throw new Exception("Historical event has been already added to this estate!");
        }
    }
}
