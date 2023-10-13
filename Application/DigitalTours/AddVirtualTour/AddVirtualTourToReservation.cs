using Domain.DigitalTours;
using Domain.EstateExhibits;
using MediatR;

namespace Application.DigitalTours.AddVirtualTour;

public record AddVirtualTourToReservation(
    ReservationId ReservationId,
    EstateId EstateId,
    string Currency,
    decimal Amount,
    TimeSpan Duration,
    string NarattionLanguage,
    DateTime OrganizedAt) : IRequest;


public record AddVirtualTourToReservationRequest(
    Guid ReservationId,
    Guid EstateId,
    string Currency,
    decimal Amount,
    TimeSpan Duration,
    string NarattionLanguage,
    DateTime OrganizedAt) : IRequest;
