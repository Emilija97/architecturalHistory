using Domain.EstateExhibits;

namespace Domain.Curators;

public class Expert
{
    public ExpertId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Biography { get; private set; } = string.Empty;

    public string ArchitecturalStyleExpertise { get; private set; } = string.Empty;

    private List<EstateId> _associatedEstates = new();
    public IReadOnlyCollection<EstateId> AssociatedEstates => _associatedEstates.AsReadOnly();

    public Expert(ExpertId id, string name, string email, string biography, string architecturalStyleExpertise)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name is required");
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentException("Email is required"); ;
        Biography = biography;
        ArchitecturalStyleExpertise = architecturalStyleExpertise;
    }

    private Expert()
    {
    }

    public void UpdateContactInfo(string email) 
    {
        if (!string.IsNullOrWhiteSpace(email))
            Email = email;
    }

    public void AssociatedWithEstate(EstateId estateId)
    {
        if(estateId is null)
            throw new ArgumentNullException(nameof(estateId));

        if(!_associatedEstates.Contains(estateId))
        {
            _associatedEstates.Add(estateId);
        }
    }

    public void RemoveAssociatedEstate(EstateId estateId)
    {
        if (estateId is null)
            throw new ArgumentNullException(nameof(estateId));

        var estate = _associatedEstates.FirstOrDefault(estateId);

        if (estate is null)
        {
            return;
        }

        _associatedEstates.Remove(estate);
    }
}
