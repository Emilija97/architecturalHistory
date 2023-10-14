using Application.Data;
using Domain.DigitalTours;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DigitalTours.AddHighlight;

internal sealed class AddHighlightToVirtualTourCommandHandler : IRequestHandler<AddHighlightToVirtualTour>
{

    private readonly IApplicationDbContext _context;
    private readonly IRepository<VirtualTour, VirtualTourId> _virtualToursRepository;

    public AddHighlightToVirtualTourCommandHandler(IApplicationDbContext context, IRepository<VirtualTour, VirtualTourId> virtualToursRepository)
    {
        _context = context;
        _virtualToursRepository = virtualToursRepository;
    }

    public async Task Handle(AddHighlightToVirtualTour request, CancellationToken cancellationToken)
    {
        var virtualTours = await _context.VirtualTours.Include(h => h.ScheduledHighlights).ToListAsync(cancellationToken);

        var virtualTour = await _virtualToursRepository.GetByIdAsync(request.VirtualTourId);

        if(virtualTour is null)
        {
            return;
        }

        var highlight = new Highlight(
            new HighlightId(Guid.NewGuid()),
            request.Description,
            request.VirtualTourId);

        virtualTour.ScheduleHighlight(highlight);
        await _context.SaveChangesAsync();
    }
}
