using Domain.DigitalTours;
using Domain.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Persistence.Configurations;

internal class HighlightConfiguration : IEntityTypeConfiguration<Highlight>
{
    public void Configure(EntityTypeBuilder<Highlight> builder)
    {

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Id).HasConversion(
            highlightId => highlightId.Value,
            value => new HighlightId(value));

        builder.Property(h => h.Description)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(h => h.MultimediaUrls)
               .HasConversion(

                    v => string.Join(';', v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
               )
               .HasColumnType("text")
               .HasColumnName("MultimediaUrlsString");

        builder.HasOne<VirtualTour>()
            .WithMany()
            .HasForeignKey(e => e.VirtualTourId)
            .IsRequired();

    }
}
