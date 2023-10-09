using Application.Data;
using Domain.Participants;
using MediatR;

namespace Application.Participants.Create;

internal class CreateParticipantCommandQueryHandler : IRequestHandler<CreateParticipantCommand>
{

    private readonly IRepository<Participant, ParticipantId> _participantRepository;

    public CreateParticipantCommandQueryHandler(IRepository<Participant, ParticipantId> participantRepository)
    {
        _participantRepository = participantRepository;
    }

    public async Task Handle(CreateParticipantCommand request, CancellationToken cancellationToken)
    {
        var participant = new Participant(
            new ParticipantId(Guid.NewGuid()),
            request.Email,
            request.FirstName,
            request.LastName);

        _participantRepository.Insert(participant);

        await _participantRepository.SaveChangesAsync();
    }
}