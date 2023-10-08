using Application.Data;
using Domain.Curators;
using MediatR;

namespace Application.Curators.Create;

internal sealed class CreateExpertCommandQueryHandler : IRequestHandler<CreateExpertCommand>
{

    private readonly IRepository<Expert> _expertRepository;

    public CreateExpertCommandQueryHandler(IRepository<Expert> expertRepository)
    {
        _expertRepository = expertRepository;
    }

    public async Task Handle(CreateExpertCommand request, CancellationToken cancellationToken)
    {
        var expert = new Expert(
            new ExpertId(Guid.NewGuid()),
            request.Name,
            request.Email,
            request.Biography,
            request.ArchitecturalStyleExpertise);

        _expertRepository.Insert(expert);

        await _expertRepository.SaveChangesAsync();
    }
}