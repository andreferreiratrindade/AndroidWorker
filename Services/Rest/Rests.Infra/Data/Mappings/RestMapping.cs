using Rests.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rests.Domain.Enums;

namespace Rests.Infra.Data.Mappings
{
    public class RestMapping : IEntityTypeConfiguration<Rest>
    {
        public void Configure(EntityTypeBuilder<Rest> builder)
        {
            builder.ToTable("Rests");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.TimeRestStart)
            .IsRequired()
            .HasColumnType("datetime2");

            builder.Property(c => c.TimeRestEnd)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(c => c.ActivityId)
                .IsRequired()
                .HasColumnType("UNIQUEIDENTIFIER");

            builder.Property(c => c.TypeActivityBuild)
                .IsRequired()
                .HasConversion(new EnumToNumberConverter<TypeActivityBuild, byte>());

            builder.Property(c => c.WorkerId)
                .IsRequired()
                .HasColumnType("char(1)");

        }
    }
}