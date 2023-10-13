using Domain.DigitalTours;
using MediatR;

namespace Application.DigitalTours.Create;

public record ReservationCreatedEvent(ReservationId ReservationId) : INotification;
