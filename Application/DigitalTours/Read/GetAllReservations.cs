using Domain.Participants;
using MediatR;

namespace Application.DigitalTours.Read;

public record GetAllReservations() : IRequest<List<ReservationResponse>>;
