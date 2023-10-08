using Domain.EstateExhibits;
using MediatR;

namespace Application.EstateExhibits.Create;

public record EstateRejectedEvent(EstateId EstateId) : INotification;
