using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Curators.Read;

internal sealed class GetAllExpertsQueryHandler : IRequestHandler<GetAllExpertsQuery, List<ExpertResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllExpertsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExpertResponse>> Handle(GetAllExpertsQuery request, CancellationToken cancellationToken)
    {
        var experts = await _context
            .Experts
            .Select(p => new ExpertResponse(
                p.Id.Value,
                p.Email,
                p.Name,
                p.Biography,
                p.ArchitecturalStyleExpertise))
            .ToListAsync(cancellationToken);

        return experts is null ? throw new Exception("There is no any expert yet!") : experts;
    }
}