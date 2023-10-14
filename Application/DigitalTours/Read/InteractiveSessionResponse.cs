namespace Application.DigitalTours.Read;

public record InteractiveSessionResponse(
    Guid Id,
    Guid ExpertId,
    DateTime SheduledTime,
    TimeSpan Duration);