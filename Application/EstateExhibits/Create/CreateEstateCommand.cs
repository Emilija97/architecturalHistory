using MediatR;

namespace Application.EstateExhibits.Create;

public record CreateEstateCommand(
    string Name,
    string ArchStyleName,
    string Period,
    string Description,
    string Latitude,
    string Longitude,
    string Address,
    int Year) : IRequest;
