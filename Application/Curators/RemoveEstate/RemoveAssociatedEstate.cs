using Domain.Curators;
using Domain.EstateExhibits;
using MediatR;

namespace Application.Curators.RemoveEstate;

public record RemoveAssociatedEstate(ExpertId ExpertId, EstateId EstateId) : IRequest;
