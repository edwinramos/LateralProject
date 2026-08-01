using LateralProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LateralProject.Infrastructure.Persistence.Configurations;

public sealed class LateralEntityConfiguration : IEntityTypeConfiguration<LateralEntity>
{
    public void Configure(EntityTypeBuilder<LateralEntity> builder)
    {
        builder.ToTable("LateralEntities");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired();

        builder.HasIndex(x => x.Description)
            .IsUnique();

        builder.Property(x => x.CreatedDateTime)
            .IsRequired();

        builder.Property(x => x.ModifiedDateTime)
            .IsRequired();
    }
}