using Domain.DigitalTours;

namespace Domain.EstateExhibits;

public class Estate
{
    public Estate(EstateId id, string name, ArchitecturalStyle architecturalStyle, YearBuilt yearBuilt, Location location)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        ArchitecturalStyle = architecturalStyle ?? throw new ArgumentNullException(nameof(architecturalStyle));
        YearBuilt = yearBuilt ?? throw new ArgumentNullException(nameof(yearBuilt));
        Location = location ?? throw new ArgumentNullException(nameof(location));
    }

    private Estate()
    {

    }

    public EstateId Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public ArchitecturalStyle ArchitecturalStyle { get; private set; }

    private readonly List<HistoricalEvent> _events = new();

    public IReadOnlyList<HistoricalEvent> Events => _events.ToList();

    private readonly List<VirtualTour> _virtualTours = new();

    public IReadOnlyList<VirtualTour> VirtualTours => _virtualTours.AsReadOnly();

    public YearBuilt YearBuilt {  get; private set; }

    public Location Location { get; private set; }

    public void AddHistoricalEvent(DateTime date, string description, string impact)
    {
        var hisEvent = new HistoricalEvent(
            new HistoricalEventId(Guid.NewGuid()),
            Id,
            date, description, impact);
       
        _events.Add(hisEvent);
    }

    public void AddVirtualTour(VirtualTour virtualTour)
    {
        _virtualTours.Add(virtualTour ?? throw new ArgumentNullException(nameof(virtualTour)));
    }

    public void RemoveVirtualTour(VirtualTourId virtualTourId)
    {
        var virtualTour = _virtualTours.FirstOrDefault(vt => vt.Id == virtualTourId);

        if (virtualTour is null)
        {
            return;
        }

        _virtualTours.Remove(virtualTour);
    }
}
