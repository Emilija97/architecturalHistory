using MediatR;

namespace Application.Participants.Create;

public record CreateParticipantCommand(
    string Email,
    string FirstName,
    string LastName) : IRequest;
