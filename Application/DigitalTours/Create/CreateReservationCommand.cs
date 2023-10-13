using Domain.EstateExhibits;
using Domain.Participants;
using MediatR;

namespace Application.DigitalTours.Create;

public record CreateReservationCommand(
    ParticipantId ParticipantId,
    EstateId EstateId,
    string Currency,
    decimal Amount,
    TimeSpan Duration,
    string NarattionLanguage,
    DateTime OrganizedAt) : IRequest;

public record CreateReservationRequest(
    Guid ParticipantId,
    Guid EstateId,
    string Currency,
    decimal Amount,
    TimeSpan Duration,
    string NarattionLanguage,
    DateTime OrganizedAt);
