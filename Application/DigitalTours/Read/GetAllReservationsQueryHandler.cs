using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DigitalTours.Read;

internal sealed class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservations, List<ReservationResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllReservationsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReservationResponse>> Handle(GetAllReservations request, CancellationToken cancellationToken)
    {
        var reservations = await _context
            .Reservations
            .Select(r => new ReservationResponse(
                r.Id.Value,
                r.ParticipantId.Value,
                r.VirtualTours
                    .Select(vt => new VirtualTourResponse(vt.Id.Value, vt.TourPrice.Amount, vt.Duration, vt.NarrationLanguage, vt.OrganizedAt))
                    .ToList()
            )).ToListAsync(cancellationToken);

        return reservations is null ? throw new Exception("There is no any reservation yet!") : reservations;
    }
}