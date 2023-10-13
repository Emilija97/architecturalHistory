using Application.Data;
using Domain.DigitalTours;
using Domain.EstateExhibits;
using Domain.Participants;
using MediatR;

namespace Application.DigitalTours.Create;

internal sealed class CreateBookingCommandHandler : IRequestHandler<CreateReservationCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IPublisher _publisher;
    private readonly IRepository<Participant, ParticipantId> _participantsRepository;
    private readonly IRepository<Reservation, ReservationId> _reservationRepository;
    private readonly IRepository<Estate, EstateId> _estateRepository;

    public CreateBookingCommandHandler(
        IApplicationDbContext context,
        IPublisher publisher,
        IRepository<Participant, ParticipantId> participantsRepository,
        IRepository<Reservation, ReservationId> reservationRepository,
        IRepository<Estate, EstateId> estateRepository)
    {
        _context = context;
        _publisher = publisher;
        _participantsRepository = participantsRepository;
        _reservationRepository = reservationRepository;
        _estateRepository = estateRepository;
    }

    public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var participant = await _participantsRepository.GetByIdAsync(request.ParticipantId);

        var estate = await _estateRepository.GetByIdAsync(request.EstateId);

        if (participant is null || estate is null)
        {
            return;
        }

        var reservation = Reservation.Create(participant.Id, estate.Id, request.Amount, request.Currency, request.Duration, request.NarattionLanguage, request.OrganizedAt.ToLocalTime());

        _reservationRepository.Insert(reservation);

        await _reservationRepository.SaveChangesAsync();

        await _publisher.Publish(new ReservationCreatedEvent(reservation.Id), cancellationToken);
    }
}
