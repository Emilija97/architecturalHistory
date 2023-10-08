using MediatR;

namespace Application.Curators.Read;

public record GetAllExpertsQuery() : IRequest<List<ExpertResponse>>;
