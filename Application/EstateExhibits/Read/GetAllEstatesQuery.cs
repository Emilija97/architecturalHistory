using MediatR;

namespace Application.EstateExhibits.Read;

public record GetAllEstatesQuery() : IRequest<List<EstateResponse>>;
    