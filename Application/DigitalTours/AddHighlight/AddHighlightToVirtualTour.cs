using Domain.DigitalTours;
using MediatR;

namespace Application.DigitalTours.AddHighlight;

public record AddHighlightToVirtualTour(VirtualTourId VirtualTourId, string Description) : IRequest;
public record AddHighlightToVirtualTourRequest(Guid VirtualTourId, string Description) : IRequest;
