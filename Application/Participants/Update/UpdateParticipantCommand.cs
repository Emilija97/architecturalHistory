using Domain.Participants;
using MediatR;

namespace Application.Participants.Update;

public record UpdateParticipantCommand(
    ParticipantId ParticipantId,
    string FirstName,
    string LastName) : IRequest;

public record UpdateParticipantRequest(
    string FirstName,
    string LastName);