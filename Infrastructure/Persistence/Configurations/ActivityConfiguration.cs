using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Type)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(a => a.Subject)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Description)
                .HasColumnType("nvarchar(max)");

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(a => a.Priority)
                .HasConversion<int>();

            builder.Property(a => a.Location)
                .HasMaxLength(200);

            builder.Property(a => a.RelatedToType)
                .HasMaxLength(50);

            // Relationships
            builder.HasOne(a => a.Tenant)
                .WithMany()
                .HasForeignKey(a => a.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Owner)
                .WithMany()
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Indexes
            builder.HasIndex(a => a.TenantId)
                .HasDatabaseName("IX_Activities_TenantId");

            builder.HasIndex(a => a.Type)
                .HasDatabaseName("IX_Activities_Type");

            builder.HasIndex(a => a.Status)
                .HasDatabaseName("IX_Activities_Status");

            builder.HasIndex(a => new { a.RelatedToType, a.RelatedToId })
                .HasDatabaseName("IX_Activities_RelatedTo");

            builder.HasIndex(a => a.OwnerId)
                .HasDatabaseName("IX_Activities_OwnerId");

            builder.HasIndex(a => a.ScheduledAt)
                .HasDatabaseName("IX_Activities_ScheduledAt");

            builder.Property(a => a.CreatedAt).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
