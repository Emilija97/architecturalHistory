using Domain.EstateExhibits;
using MediatR;

namespace Application.EstateExhibits.Create;

public record EstateCreatedEvent(EstateId EstateId) : INotification;