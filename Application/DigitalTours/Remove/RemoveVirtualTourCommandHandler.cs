using Application.Data;
using Application.DigitalTours.Remove;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bookings.RemoveTicket;

internal sealed class RemoveVirtualTourCommandHandler : IRequestHandler<RemoveVirtualTour>
{
    private readonly IApplicationDbContext _context;

    public RemoveVirtualTourCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveVirtualTour request, CancellationToken cancellationToken)
    {
        var reservation = await _context
            .Reservations
            .Include(r => r.VirtualTours.Where(vt => vt.Id == request.VirtualTourId))
            .SingleOrDefaultAsync(r => r.Id == request.ReservationId, cancellationToken);

        if (reservation is null)
        {
            return;
        }

        reservation.RemoveVirtualTour(request.VirtualTourId);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
