using Domain.Curators;
using Domain.DigitalTours;
using Domain.EstateExhibits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class InteractiveSessionConfiguration : IEntityTypeConfiguration<InteractiveSession>
{
    public void Configure(EntityTypeBuilder<InteractiveSession> builder)
    {
        builder.HasKey(isess => isess.Id);

        builder.Property(isess => isess.Id).HasConversion(
            interactiveSessionId => interactiveSessionId.Value,
            value => new InteractiveSessionId(value));

        builder.Property(isess => isess.SheduledTime)
              .IsRequired();

        builder.Property(isess => isess.Duration)
               .IsRequired();

        builder.HasOne<VirtualTour>()
               .WithMany()
               .HasForeignKey(isess => isess.VirtualTourId);

        builder.HasOne<Expert>()
               .WithMany()
               .HasForeignKey(isess => isess.ExpertId);
    }
}
