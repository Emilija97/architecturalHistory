using MediatR;

namespace Application.Curators.Create;

public record CreateExpertCommand(
    string Email,
    string Name,
    string Biography,
    string ArchitecturalStyleExpertise) : IRequest;
