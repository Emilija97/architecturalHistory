using Domain.EstateExhibits;
using MediatR;

namespace Application.EstateExhibits.Remove;

public record RemoveMultimediaContent(HistoricalEventId HistoricalEventId, MultimediaContentId MultimediaContentId) : IRequest;
