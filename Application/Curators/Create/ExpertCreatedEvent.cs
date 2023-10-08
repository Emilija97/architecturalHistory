using Domain.Curators;
using MediatR;

namespace Application.Curators.Create;

public record ExpertCreatedEvent(ExpertId ExpertId) : INotification;
