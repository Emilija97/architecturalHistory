using Domain.Curators;
using Domain.DigitalTours;
using MediatR;

namespace Application.DigitalTours.AddInteractiveSession;

public record AddInteractiveSessionToVirtualTour(
    VirtualTourId VirtualTourId,
    ExpertId ExpertId,
    DateTime SheduledTime,
    TimeSpan Duration) : IRequest;
public record AddInteractiveSessionToVirtualTourRequest(
    Guid VirtualTourId,
    Guid ExpertId,
    DateTime SheduledTime,
    TimeSpan Duration) : IRequest;