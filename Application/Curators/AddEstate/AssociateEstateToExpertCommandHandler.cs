using Application.Curators.Create;
using Application.Data;
using Domain.Curators;
using Domain.EstateExhibits;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Curators.AddEstate;

internal sealed class AssociateEstateToExpertCommandHandler : IRequestHandler<AssociateEstateToExpert>
{

    private readonly IApplicationDbContext _context;
    private readonly IPublisher _publisher;
    private readonly IRepository<Expert> _expertRepository;
    private readonly IRepository<Estate> _estateRepository;

    public AssociateEstateToExpertCommandHandler(
        IApplicationDbContext context,
        IPublisher publisher,
        IRepository<Expert> expertRepository,
        IRepository<Estate> EstateRepository)
    {
        _context = context;
        _publisher = publisher;
        _expertRepository = expertRepository;
        _estateRepository = EstateRepository;
    }

    public async Task Handle(AssociateEstateToExpert request, CancellationToken cancellationToken)
    {

        var experts = await _context.Experts.Include(e => e.AssociatedEstates).ToListAsync(cancellationToken);

        var expert = await _expertRepository.GetByIdAsync(request.ExpertId.Value);

        var estate = await _estateRepository.GetByIdAsync(request.EstateId.Value);

        if (expert is null || estate is null)
        {
            return;
        }

        var contains = false;
        if (expert.AssociatedEstates.Contains(request.EstateId)) contains = true;

        if (!contains)
        {
            expert.AssociatedWithEstate(request.EstateId);
            await _context.SaveChangesAsync();
            await _publisher.Publish(new ExpertCreatedEvent(expert.Id), cancellationToken);
        }
        else
        {
            await _publisher.Publish(new ExpertRejectedEvent(expert.Id), cancellationToken);
            throw new Exception("Estate ia alreade associated with this Expert!");
        }
    }
}
