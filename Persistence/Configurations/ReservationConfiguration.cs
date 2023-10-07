using Domain.DigitalTours;
using Domain.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id).HasConversion(
            reservationId => reservationId.Value,
            value => new ReservationId(value));

        builder.HasOne<Participant>()
            .WithMany()
            .HasForeignKey(r => r.ParticipantId)
            .IsRequired();

        builder.HasMany(r => r.VirtualTours)
            .WithOne()
            .HasForeignKey(vt => vt.ReservationId);
    }
}
