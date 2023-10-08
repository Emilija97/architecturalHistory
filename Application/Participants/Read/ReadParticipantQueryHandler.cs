using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Participants.Read;

internal sealed class ReadParticipantQueryHandler : IRequestHandler<ReadParticipantQuery, ParticipantResponse>
{
    private readonly IApplicationDbContext _context;

    public ReadParticipantQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ParticipantResponse> Handle(ReadParticipantQuery request, CancellationToken cancellationToken)
    {
        var participant = await _context
            .Participants
            .Where(p => p.Id == request.ParticipantId)
            .Select(p => new ParticipantResponse(
                p.Id.Value,
                p.Email,
                p.FirstName,
                p.LastName))
            .FirstOrDefaultAsync(cancellationToken);

        if(participant is null)
        {
            throw new Exception("Participant with given id does not exist!");
        }

        return participant;
    }
}
