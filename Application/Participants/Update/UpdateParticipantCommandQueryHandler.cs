using Application.Data;
using Domain.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Participants.Update;

internal sealed class UpdateParticipantCommandQueryHandler : IRequestHandler<UpdateParticipantCommand>
{
    private readonly IRepository<Participant> _participantRepository;

    private readonly IApplicationDbContext _context;

    public UpdateParticipantCommandQueryHandler(IRepository<Participant> participantRepository, IApplicationDbContext context)
    {
        _context = context;
        _participantRepository = participantRepository;
    }

    public async Task Handle(UpdateParticipantCommand request, CancellationToken cancellationToken)
    {
        //var participant = await _participantRepository.GetByIdAsync(request.ParticipantId.Value);
        var participantId = request.ParticipantId.Value;

        var participant = await _context.Participants.SingleOrDefaultAsync(p => p.Id == new ParticipantId(participantId));

        if (participant is null)
        {
            throw new ArgumentNullException(nameof(participant));
        }

        participant.Update(
            request.FirstName,
            request.LastName);

        _participantRepository.Update(participant);

        await _participantRepository.SaveChangesAsync();
    }
}
