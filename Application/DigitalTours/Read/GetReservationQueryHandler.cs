using Application.Data;
using Domain.DigitalTours;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DigitalTours.Read;

internal sealed class GetReservationQueryHandler :
    IRequestHandler<GetReservationQuery, ReservationResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IRepository<Reservation, ReservationId> _reservationRepository;

    public GetReservationQueryHandler(IApplicationDbContext context, IRepository<Reservation, ReservationId> reservationRepository)
    {
        _context = context;
        _reservationRepository = reservationRepository;
    }

    public async Task<ReservationResponse> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        var reservationResponse = await _reservationRepository
            .GetQueryable()
            .Where(r => r.Id == new ReservationId(request.ReservationId))
            .Select(r => new ReservationResponse(
                r.Id.Value,
                r.ParticipantId.Value,
                r.VirtualTours
                    .Select(vt => new VirtualTourResponse(vt.Id.Value,
                                                          vt.TourPrice.Amount,
                                                          vt.Duration,
                                                          vt.NarrationLanguage,
                                                          vt.OrganizedAt,
                                                          vt.ScheduledHighlights.Select(h => new HighlightResponse(h.Id.Value, h.Description)).ToList(),
                                                          vt.ScheduledSessions.Select(isess => new InteractiveSessionResponse(isess.Id.Value, isess.ExpertId.Value, isess.SheduledTime, isess.Duration)).ToList()))
                    .ToList()))
            .SingleAsync(cancellationToken);

        return reservationResponse;
    }
}
