using MediatR;

namespace Application.Participants.Read;

public record GetAllParticipantsQuery() : IRequest<List<ParticipantResponse>>;
