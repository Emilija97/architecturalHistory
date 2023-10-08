using Domain.Curators;
using Domain.Participants;
using MediatR;

namespace Application.Curators.Read;

public record ReadExpertQuery(ExpertId ExpertId) : IRequest<ExpertResponse>;

public record ExpertResponse(
    Guid Id,
    string Email,
    string Name,
    string Biography,
    string ArchitecturalStyleExpertise);
