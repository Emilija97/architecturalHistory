using Domain.EstateExhibits;
using Domain.Participants;

namespace Domain.DigitalTours;

public class Reservation
{
    public ReservationId Id { get; private set; }
    
    public ParticipantId ParticipantId { get; private set; }

    private readonly List<VirtualTour> _virtualTours = new();

    private Reservation()
    {
    }

    public IReadOnlyList<VirtualTour> VirtualTours => _virtualTours.ToList();

    public static Reservation Create(ParticipantId participantId, EstateId estateId, decimal Amount, string Currency, TimeSpan duration, string narattionLanguage)
    {
        var reservation = new Reservation
        {
            Id = new ReservationId(Guid.NewGuid()),
            ParticipantId = participantId,
        };

        reservation.Add(estateId, new Price(Amount, Currency), duration, narattionLanguage);

        return reservation;
    }

    public void Add(EstateId estateId, Price price, TimeSpan duration, string narattionLanguage)
    {
        var virtualTour = new VirtualTour(
            new VirtualTourId(Guid.NewGuid()),
            estateId,
            Id,
            duration,
            narattionLanguage,
            price
            );

        _virtualTours.Add(virtualTour);
    }

    public void RemoveVirtualTour(VirtualTourId virtualTourId)
    {
        var virtualTour = _virtualTours.FirstOrDefault(vt => vt.Id == virtualTourId);

        if (virtualTour is null)
        {
            return;
        }

        _virtualTours.Remove(virtualTour);
    }
}
