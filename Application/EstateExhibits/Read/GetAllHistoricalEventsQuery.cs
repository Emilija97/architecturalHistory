using Domain.EstateExhibits;
using MediatR;

namespace Application.EstateExhibits.Read;

public record GetAllHistoricalEventsQuery(EstateId EstateId) : IRequest<List<HistoricalEventResponse>>;
