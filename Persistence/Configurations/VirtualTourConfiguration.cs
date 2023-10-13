using Domain.DigitalTours;
using Domain.EstateExhibits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;

namespace Persistence.Configurations;

internal class VirtualTourConfiguration : IEntityTypeConfiguration<VirtualTour>
{
    public void Configure(EntityTypeBuilder<VirtualTour> builder)
    {
        builder.HasKey(vt => vt.Id);

        builder.Property(vt => vt.Id).HasConversion(
                virtualTourId => virtualTourId.Value,
                value => new VirtualTourId(value));

        builder.Property(vt => vt.Duration)
               .IsRequired();

        builder.Property(vt => vt.NarrationLanguage)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(vt => vt.OrganizedAt)
            .IsRequired();

        //builder.HasOne<Estate>()
        //    .WithMany(e => e.VirtualTours)
        //    .HasForeignKey(vt => vt.EstateId);


        builder.OwnsOne(vt => vt.TourPrice, tourPriceBuilder =>
        {
            tourPriceBuilder.Property(p => p.Amount).IsRequired();
            tourPriceBuilder.Property(pr => pr.Currency).HasMaxLength(3);
        });

        // Relationships
        builder.HasMany(vt => vt.ScheduledHighlights)
               .WithOne()
               .HasForeignKey(h => h.VirtualTourId);

        builder.HasMany(vt => vt.ScheduledSessions)
               .WithOne()
               .HasForeignKey(s => s.VirtualTourId);
    }
}
