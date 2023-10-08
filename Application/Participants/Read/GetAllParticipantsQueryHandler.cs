using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Participants.Read;

internal sealed class GetAllParticipantsQueryHandler : IRequestHandler<GetAllParticipantsQuery, List<ParticipantResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllParticipantsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ParticipantResponse>> Handle(GetAllParticipantsQuery request, CancellationToken cancellationToken)
    {
        var participants = await _context
            .Participants
            .Select(p => new ParticipantResponse(
                p.Id.Value,
                p.Email,
                p.FirstName,
                p.LastName))
            .ToListAsync(cancellationToken);

        return participants is null ? throw new Exception("There is no any participant yet!") : participants;
    }
}