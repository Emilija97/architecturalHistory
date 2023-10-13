namespace Application.DigitalTours.Read;

public record VirtualTourResponse(Guid VirtualTourId, decimal Price, TimeSpan Duration, string NarrationLanguage, DateTime OrganizedAt);
