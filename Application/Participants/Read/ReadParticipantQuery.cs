using Domain.Participants;
using MediatR;

namespace Application.Participants.Read;

public record ReadParticipantQuery(ParticipantId ParticipantId) : IRequest<ParticipantResponse>;

public record ParticipantResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);

