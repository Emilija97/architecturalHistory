using Domain.EstateExhibits;
using MediatR;

namespace Application.EstateExhibits.AddMultimediaContent;

public record AddHistoricalEventToEstate(EstateId EstateId, DateTime Date, string Description, string Impact) : IRequest;