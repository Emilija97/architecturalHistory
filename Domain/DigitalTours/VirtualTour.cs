using Domain.EstateExhibits;

namespace Domain.DigitalTours;

public class VirtualTour
{
    public VirtualTourId Id { get; private set; }

    public EstateId EstateId { get; private set; }

    public ReservationId ReservationId { get; private set; }

    public TimeSpan Duration { get; private set; }

    public string NarrationLanguage { get; private set; } = "en";

    public Price TourPrice { get; private set; }

    private List<Highlight> _scheduledHighlights = new();

    private List<InteractiveSession> _scheduledSessions = new();

    internal VirtualTour(VirtualTourId virtualTourId, EstateId estateId, ReservationId reservationId, TimeSpan duration, string narrationLanguage, Price tourPrice)
    {
        Id = virtualTourId;
        EstateId = estateId;
        ReservationId = reservationId;
        Duration = duration;
        NarrationLanguage = narrationLanguage;
        TourPrice = tourPrice;
    }

    private VirtualTour()
    {
    }

    public IReadOnlyList<Highlight> ScheduledHighlights => _scheduledHighlights.AsReadOnly();
    public IReadOnlyList<InteractiveSession> ScheduledSessions => _scheduledSessions.AsReadOnly();

    public void ScheduleHighlight(Highlight highlight)
    {
        if (highlight == null) throw new ArgumentNullException(nameof(highlight));
        _scheduledHighlights.Add(highlight);
    }

    public void ScheduleInteractiveSession(InteractiveSession session)
    {
        if (session == null) throw new ArgumentNullException(nameof(session));
        _scheduledSessions.Add(session);
    }
}
