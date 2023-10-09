using Domain.EstateExhibits;

namespace Application.EstateExhibits.Read;

public record HistoricalEventResponse(HistoricalEventId HistoricalEventId, DateTime Date, string Description, string Impact);
