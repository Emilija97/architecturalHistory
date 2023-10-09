using Application.Data;
using Domain.Curators;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Curators.Update;

internal class UpdateExpertCommandQueryHandler : IRequestHandler<UpdateExpertCommand>
{
    private readonly IRepository<Expert, ExpertId> _expertRepository;

    private readonly IApplicationDbContext _context;

    public UpdateExpertCommandQueryHandler(IRepository<Expert, ExpertId> expertRepository, IApplicationDbContext context)
    {
        _context = context;
        _expertRepository = expertRepository;
    }

    public async Task Handle(UpdateExpertCommand request, CancellationToken cancellationToken)
    {
        var expertId = request.ExpertId.Value;

        var expert = await _context.Experts.SingleOrDefaultAsync(p => p.Id == new ExpertId(expertId));

        if (expert is null)
        {
            throw new ArgumentNullException(nameof(expert));
        }

        expert.UpdateContactInfo(
            request.Name,
            request.Biography);

        _expertRepository.Update(expert);

        await _expertRepository.SaveChangesAsync();
    }
}

