using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Curators.Read;

internal class ReadExpertQueryHandler : IRequestHandler<ReadExpertQuery, ExpertResponse>
{
    private readonly IApplicationDbContext _context;

    public ReadExpertQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ExpertResponse> Handle(ReadExpertQuery request, CancellationToken cancellationToken)
    {
        var expert = await _context
            .Experts
            .Where(p => p.Id == request.ExpertId)
            .Select(p => new ExpertResponse(
                p.Id.Value,
                p.Email,
                p.Name,
                p.Biography,
                p.ArchitecturalStyleExpertise))
            .FirstOrDefaultAsync(cancellationToken);

        if (expert is null)
        {
            throw new Exception("Expert with given id does not exist!");
        }

        return expert;
    }
}