﻿using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.DigitalTours.Read;

internal class GetAllReservationsForParticipantQueryHandler : IRequestHandler<GetAllReservationsForParticipant, List<ReservationResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllReservationsForParticipantQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReservationResponse>> Handle(GetAllReservationsForParticipant request, CancellationToken cancellationToken)
    {
        var reservations = await _context
            .Reservations
            .Where(r => r.ParticipantId == request.ParticipantId)
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
                    .ToList()
            )).ToListAsync(cancellationToken);

        return reservations is null ? throw new Exception("Participant with given id hasn't reserved any virtual tour!") : reservations;
    }
}

