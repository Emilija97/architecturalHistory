using Domain.EstateExhibits;
using MediatR;

namespace Application.EstateExhibits.Read;

public record ReadEstateQuery(EstateId EstateId) : IRequest<EstateResponse>;

public record EstateResponse(
    Guid Id,
    string Name,
    string ArchStyleName,
    string Period,
    string Description,
    string Latitude,
    string Longitude,
    string Address,
    int Year,
    List<HistoricalEventResponse> HistoricalEventResponses);
