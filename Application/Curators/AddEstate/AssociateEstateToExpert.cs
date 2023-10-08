using Domain.Curators;
using Domain.EstateExhibits;
using MediatR;

namespace Application.Curators.AddEstate;

public record AssociateEstateToExpert(ExpertId ExpertId, EstateId EstateId) : IRequest;
