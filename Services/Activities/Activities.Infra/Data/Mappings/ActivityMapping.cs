using Activities.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Activities.Domain.Enums;

namespace Activities.Infra.Data.Mappings
{
    public class ActivityMapping : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities");
            
            builder.HasKey(c => c.Id);

            builder.Property(c => c.TimeActivityStart)
                .IsRequired()
                .HasColumnType("datetime2");
            
            builder.Property(c => c.TimeActivityEnd)
                .IsRequired()
                .HasColumnType("datetime2");
            
                        
            builder.Property(c => c.TimeRestEnd)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(c => c.IsAlive)
                .IsRequired()
                .HasColumnType("bit");
                        
            builder.Property(c => c.TypeActivityBuild)
                .IsRequired()
                .HasConversion(new EnumToNumberConverter<TypeActivityBuild, byte>());
            
            builder.OwnsMany<WorkActivity>("_workers", y =>
            {
                y.ToTable("WorkersActivity");
                y.Property<Guid>("ActivityId").HasColumnType("UNIQUEIDENTIFIER").IsRequired();
                y.Property<string>("WorkerId").HasColumnType("char(1)").IsRequired();

                y.HasKey("ActivityId", "WorkerId");

            });

                
        }
    }
}