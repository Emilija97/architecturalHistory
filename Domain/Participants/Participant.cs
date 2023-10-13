using Domain.Shared;

namespace Domain.Participants;

public class Participant : IEntityWithGuidId
{
    private Participant()
    {
    }

    public Participant(ParticipantId id, string email, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public Participant(Guid id, string email, string firstName, string lastName)
    {
        Id = new ParticipantId(id);
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public ParticipantId Id { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid GetGuidId()
    {
        return Id.Value;
    }
}
