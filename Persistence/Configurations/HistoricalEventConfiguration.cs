using Domain.EstateExhibits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class HistoricalEventConfiguration : IEntityTypeConfiguration<HistoricalEvent>
{
    public void Configure(EntityTypeBuilder<HistoricalEvent> builder)
    {
        builder.HasKey(he => he.Id);

        builder.Property(he => he.Id).HasConversion(
            historicalEventId => historicalEventId.Value,
            value => new HistoricalEventId(value));

        builder.Property(he => he.Date)
               .IsRequired();

        builder.Property(he => he.Description)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(he => he.Impact)
               .HasMaxLength(500);

        // One-to-many relationship
        builder.HasMany(he => he.MultimediaContents)
               .WithOne()
               .HasForeignKey(mc => mc.HistoricalEventId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
