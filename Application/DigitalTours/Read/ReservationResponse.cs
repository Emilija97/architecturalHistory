namespace Application.DigitalTours.Read;

public record ReservationResponse(Guid Id, Guid ParticipantId, List<VirtualTourResponse> VirtualTours);
