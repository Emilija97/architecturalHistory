using Domain.Curators;
using Domain.EstateExhibits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class ExpertConfiguration : IEntityTypeConfiguration<Expert>
{
    public void Configure(EntityTypeBuilder<Expert> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasConversion(
            expertId => expertId.Value,
            value => new ExpertId(value));

        builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(e => e.Biography)
            .HasMaxLength(500);

        builder.Property(e => e.ArchitecturalStyleExpertise)
                .HasMaxLength(500);

        builder.OwnsMany(e => e.AssociatedEstates, a =>
        {
            a.WithOwner().HasForeignKey("ExpertId");
            a.Property<Guid>("Value").HasColumnName("EstateId");
            a.HasKey("Value");
        });
    }
}
