using Domain.Curators;

namespace Domain.DigitalTours;

//public record InteractiveSession(ExpertId ExpertId, DateTime ScheduledTime, TimeSpan Duration);

public class InteractiveSession
{
    private InteractiveSession()
    {
    }

    public InteractiveSession(InteractiveSessionId id, VirtualTourId virtualTourId, ExpertId expertId, DateTime sheduledTime, TimeSpan duration)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        VirtualTourId = virtualTourId ?? throw new ArgumentNullException(nameof(virtualTourId));
        ExpertId = expertId ?? throw new ArgumentNullException(nameof(expertId));
        SheduledTime = sheduledTime;
        Duration = duration;
    }

    public InteractiveSessionId Id { get; private set; }

    public VirtualTourId VirtualTourId { get; private set;}

    public ExpertId ExpertId { get; private set; }
    
    public DateTime SheduledTime { get; private set; }

    public TimeSpan Duration { get; private set; }
}