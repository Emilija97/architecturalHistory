using Domain.DigitalTours;

namespace Domain.EstateExhibits;

public class HistoricalEvent
{
    public HistoricalEvent(HistoricalEventId id, EstateId estatetId, DateTime date, string description, string impact) 
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        EstateId = estatetId;
        Date = date;
        Description = description;
        Impact = impact;
    }
    private HistoricalEvent() 
    { }

    public HistoricalEventId Id { get; private set; }

    public EstateId EstateId { get; private set; }

    public DateTime Date { get; private set; }  =   DateTime.Now;

    public string Description { get; private set; } = string.Empty;

    public string Impact { get; private set; } = string.Empty;


    private readonly List<MultimediaContent> _multimediaContents = new();

    public IReadOnlyList<MultimediaContent> MultimediaContents => _multimediaContents;

    public void UpdateImpact(string impact)
    {
        Impact = impact ?? throw new ArgumentNullException(nameof(impact));
    }

    public void AddMultimediaContent(string url, DateTime creationDate)
    {
        var multimediaContent = new MultimediaContent(
            new MultimediaContentId(Guid.NewGuid()),
            Id,
            url,
            creationDate);

        _multimediaContents.Add(multimediaContent);
    }

    public void RemoveMultimediaContent(MultimediaContent content)
    {
        var multimediaContent = _multimediaContents.FirstOrDefault(he => he.Id == content.Id);

        if (multimediaContent is null)
        {
            return;
        }

        _multimediaContents.Remove(content);
    }
}
