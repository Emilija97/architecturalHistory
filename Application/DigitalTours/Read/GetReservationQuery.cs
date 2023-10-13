using MediatR;

namespace Application.DigitalTours.Read;

public record GetReservationQuery(Guid ReservationId) : IRequest<ReservationResponse>;
