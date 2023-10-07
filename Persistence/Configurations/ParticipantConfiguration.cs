using Domain.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
            participantId => participantId.Value,
            value => new ParticipantId(value));

        builder.Property(p => p.FirstName).HasMaxLength(100);

        builder.Property(p => p.LastName).HasMaxLength(100);

        builder.Property(p => p.Email).HasMaxLength(255);

        builder.HasIndex(p => p.Email).IsUnique();
    }
}
