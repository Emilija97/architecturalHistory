using Domain.EstateExhibits;

namespace Application.EstateExhibits.Read;

public record MultimediaContentResponse(Guid Id, string Url, DateTime CreationDate);