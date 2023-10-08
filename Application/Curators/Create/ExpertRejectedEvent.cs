using Domain.Curators;
using MediatR;

namespace Application.Curators.Create;

public record ExpertRejectedEvent(ExpertId ExpertId) : INotification;
