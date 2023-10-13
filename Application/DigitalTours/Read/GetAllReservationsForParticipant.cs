using Domain.Participants;
using MediatR;

namespace Application.DigitalTours.Read;

public record GetAllReservationsForParticipant(ParticipantId ParticipantId) : IRequest<List<ReservationResponse>>;