using Domain.DigitalTours;
using Domain.EstateExhibits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class EstateConfiguration : IEntityTypeConfiguration<Estate>
{
    public void Configure(EntityTypeBuilder<Estate> builder)
    {
        builder.HasKey(es => es.Id);

        builder.Property(e => e.Id).HasConversion(
                estateId => estateId.Value,
                value => new EstateId(value));

        builder.Property(e => e.Name)
               .HasMaxLength(250) 
               .IsRequired();

        builder.OwnsOne(e => e.ArchitecturalStyle, style =>
        {
            style.Property(s => s.Name).HasMaxLength(250).IsRequired();
            style.Property(s => s.Period).HasMaxLength(250).IsRequired();
            style.Property(s => s.Description).HasMaxLength(500).IsRequired();
        });

        builder.OwnsOne(e => e.YearBuilt, year =>
        {
            year.Property(y => y.Year).IsRequired();
        });

        builder.OwnsOne(e => e.Location, loc =>
        {
            loc.Property(l => l.Latitude).HasMaxLength(50).IsRequired();
            loc.Property(l => l.Longitude).HasMaxLength(50).IsRequired();
            loc.Property(l => l.Address).HasMaxLength(500).IsRequired();
        });

        builder.HasMany(es => es.Events)
              .WithOne()
              .HasForeignKey(he => he.EstateId);

        builder.HasMany(es => es.VirtualTours)
              .WithOne()
              .HasForeignKey(vt => vt.EstateId);
    }
}
