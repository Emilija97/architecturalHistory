using Domain.Curators;
using MediatR;

namespace Application.Curators.Update;

public record UpdateExpertCommand(
    ExpertId ExpertId,
    string Name,
    string Biography) : IRequest;

public record UpdateExpertRequest(
    string Name,
    string Biography);
