using Domain.EstateExhibits;

namespace Domain.DigitalTours;

//public record Highlight(string Description, string MultimediaContent);

public class Highlight
{
    private Highlight()
    {
    }

    public Highlight(HighlightId id, string description, VirtualTourId virtualTourId)
    {
        Id = id;
        Description = description;
        VirtualTourId = virtualTourId;
    }

    public HighlightId Id { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public VirtualTourId VirtualTourId { get; private set;}

    private List<string> _multimediaUrls = new();
    public IReadOnlyList<string> MultimediaUrls => _multimediaUrls.AsReadOnly();


    public void UpdateDescription(string description)
    {
        if(!string.IsNullOrWhiteSpace(description))
            Description = description;
    }

    public void AddMultimediaUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("The URL cannot be null or empty.", nameof(url));

        if (!_multimediaUrls.Contains(url))
        {
            _multimediaUrls.Add(url);
        }
        else
        {
            throw new InvalidOperationException("This URL is already added to the highlight.");
        }
    }

    public void RemoveMultimediaUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("The URL cannot be null or empty.", nameof(url));

        if (!_multimediaUrls.Remove(url))
        {
            throw new InvalidOperationException("This URL was not found in the highlight.");
        }
    }
}
