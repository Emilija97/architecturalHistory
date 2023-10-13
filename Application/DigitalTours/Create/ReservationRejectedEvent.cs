using Domain.DigitalTours;
using MediatR;

namespace Application.DigitalTours.Create;

public record ReservationRejectedEvent(ReservationId ReservationId) : INotification;
