using Application.Data;
using Application.DigitalTours.Create;
using Domain.Curators;
using Domain.DigitalTours;
using Domain.EstateExhibits;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DigitalTours.AddVirtualTour;

internal sealed class AddVirtualTourToReservationCommandHandler : IRequestHandler<AddVirtualTourToReservation>
{

    private readonly IApplicationDbContext _context;
    private readonly IPublisher _publisher;
    private readonly IRepository<Reservation, ReservationId> _reservationRepository;
    private readonly IRepository<Estate, EstateId> _estateRepository;

    public AddVirtualTourToReservationCommandHandler(
        IApplicationDbContext context,
        IPublisher publisher,
        IRepository<Reservation, ReservationId> reservationRepository,
        IRepository<Estate, EstateId> estateRepository)
    {
        _context = context;
        _publisher = publisher;
        _reservationRepository = reservationRepository;
        _estateRepository = estateRepository;
    }

    public async Task Handle(AddVirtualTourToReservation request, CancellationToken cancellationToken)
    {
        var reservations = await _context.Reservations.Include(r => r.VirtualTours).ToListAsync(cancellationToken);

        var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);

        var estate = await _estateRepository.GetByIdAsync(request.EstateId);

        if (reservation is null || estate is null)
        {
            return;
        }

        var reserved = false;
        for (var i = 0; i < reservations.Count; i++)
        {
            for (var j = 0; j < reservations[i].VirtualTours.Count; j++)
            {
                if ((reservations[i].VirtualTours[j].EstateId == request.EstateId) && (reservations[i].VirtualTours[j].OrganizedAt.Equals(request.OrganizedAt)))
                {
                    reserved = true;
                    break;
                }
            }
        }

        if (!reserved)
        {
            reservation.Add(request.EstateId, new Price(request.Amount, request.Currency), request.Duration, request.NarattionLanguage, request.OrganizedAt);
            await _context.SaveChangesAsync();
            await _publisher.Publish(new ReservationCreatedEvent(reservation.Id), cancellationToken);
        }
        else
        {
            await _publisher.Publish(new ReservationRejectedEvent(reservation.Id), cancellationToken);
            throw new Exception("Virtual Tour has been already reserved for this time and estate!");
        }
    }
}
