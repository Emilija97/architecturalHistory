using Domain.EstateExhibits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class MultimediaContentConfiguration : IEntityTypeConfiguration<MultimediaContent>
{
    public void Configure(EntityTypeBuilder<MultimediaContent> builder)
    {
        builder.HasKey(mc => mc.Id);

        builder.Property(mc => mc.Id).HasConversion(
            multimediaContentId => multimediaContentId.Value,
            value => new MultimediaContentId(value));

        builder.Property(e => e.Url)
               .IsRequired()
               .HasMaxLength(250);

        builder.Property(e => e.CreationDate)
               .IsRequired();
    }
}
