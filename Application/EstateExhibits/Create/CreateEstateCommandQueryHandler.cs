using Application.Data;
using Domain.EstateExhibits;
using MediatR;

namespace Application.EstateExhibits.Create;

internal sealed class CreateEstateCommandHandler : IRequestHandler<CreateEstateCommand>
{
    private readonly IRepository<Estate> _estateRepository;

    public CreateEstateCommandHandler(IRepository<Estate> estateRepository)
    {
        _estateRepository = estateRepository;
    }

    public async Task Handle(CreateEstateCommand request, CancellationToken cancellationToken)
    {
        var estate = new Estate(
            new EstateId(Guid.NewGuid()),
            request.Name,
            new ArchitecturalStyle(request.ArchStyleName, request.Period, request.Description),
            new YearBuilt(request.Year),
            new Location(request.Latitude, request.Longitude, request.Address)
            );
        _estateRepository.Insert(estate);

        await _estateRepository.SaveChangesAsync();
    }
}
