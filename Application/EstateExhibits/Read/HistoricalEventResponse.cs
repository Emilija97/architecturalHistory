using Domain.EstateExhibits;

namespace Application.EstateExhibits.Read;

public record HistoricalEventResponse(Guid Id,DateTime Date, string Description, string Impact, List<MultimediaContentResponse> MultimediaContentResponses);
