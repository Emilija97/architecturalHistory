namespace Domain.EstateExhibits;

public class MultimediaContent
{
    internal MultimediaContent(MultimediaContentId id, HistoricalEventId historicalId, string url, DateTime creationDate)
    {
        Id = id;
        Url = url ?? throw new ArgumentNullException(nameof(url));
        CreationDate = creationDate;
        HistoricalEventId = historicalId;
    }

    private MultimediaContent()
    {
    }

    public MultimediaContentId Id { get; private set; }

    public string Url { get; private set; } = string.Empty;

    public DateTime CreationDate { get; private set; }

    public HistoricalEventId HistoricalEventId { get; private set; }
}
