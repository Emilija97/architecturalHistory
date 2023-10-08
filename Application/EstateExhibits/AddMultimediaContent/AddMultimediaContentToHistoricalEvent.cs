using Domain.EstateExhibits;
using MediatR;

namespace Application.EstateExhibits.AddMultimediaContent;

public record AddMultimediaContentToHistoricalEvent(HistoricalEventId HistoricalEventId, string Url, DateTime CreationDate) : IRequest;