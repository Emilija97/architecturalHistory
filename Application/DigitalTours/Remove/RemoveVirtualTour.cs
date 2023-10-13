using Domain.DigitalTours;
using MediatR;

namespace Application.DigitalTours.Remove;

public record RemoveVirtualTour(ReservationId ReservationId, VirtualTourId VirtualTourId) : IRequest;
