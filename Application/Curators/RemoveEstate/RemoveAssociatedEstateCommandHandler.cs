using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Curators.RemoveEstate;

internal sealed class RemoveAssociatedEstateCommandHandler : IRequestHandler<RemoveAssociatedEstate>
{
    private readonly IApplicationDbContext _context;

    public RemoveAssociatedEstateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveAssociatedEstate request, CancellationToken cancellationToken)
    {
        var expert = await _context
            .Experts
            .Include(ex => ex.AssociatedEstates.Where(esId => esId == request.EstateId))
            .SingleOrDefaultAsync(ex => ex.Id == request.ExpertId, cancellationToken);

        if (expert is null)
        {
            return;
        }

        expert.RemoveAssociatedEstate(request.EstateId);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
