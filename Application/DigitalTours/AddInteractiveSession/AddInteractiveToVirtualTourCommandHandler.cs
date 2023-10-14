using Application.Data;
using Application.DigitalTours.AddHighlight;
using Domain.DigitalTours;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DigitalTours.AddInteractiveSession;

internal class AddInteractiveToVirtualTourCommandHandler : IRequestHandler<AddInteractiveSessionToVirtualTour>
{

    private readonly IApplicationDbContext _context;
    private readonly IRepository<VirtualTour, VirtualTourId> _virtualToursRepository;

    public AddInteractiveToVirtualTourCommandHandler(IApplicationDbContext context, IRepository<VirtualTour, VirtualTourId> virtualToursRepository)
    {
        _context = context;
        _virtualToursRepository = virtualToursRepository;
    }

    public async Task Handle(AddInteractiveSessionToVirtualTour request, CancellationToken cancellationToken)
    {
        var virtualTours = await _context.VirtualTours.Include(h => h.ScheduledSessions).ToListAsync(cancellationToken);

        var virtualTour = await _virtualToursRepository.GetByIdAsync(request.VirtualTourId);

        if (virtualTour is null)
        {
            return;
        }

        var interactiveSession = new InteractiveSession(
            new InteractiveSessionId(Guid.NewGuid()),
            request.VirtualTourId,
            request.ExpertId,
            request.SheduledTime.ToUniversalTime(),
            request.Duration);

        virtualTour.ScheduleInteractiveSession(interactiveSession);
        await _context.SaveChangesAsync();
    }
}
