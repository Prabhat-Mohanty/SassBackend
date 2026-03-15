using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Subdomain)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.ContactEmail)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.SubscriptionTier)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(t => t.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(t => t.MaxUsers)
                .IsRequired()
                .HasDefaultValue(5);

            // Indexes
            builder.HasIndex(t => t.Subdomain)
                .IsUnique()
                .HasDatabaseName("IX_Tenants_Subdomain");

            builder.HasIndex(t => t.IsActive)
                .HasDatabaseName("IX_Tenants_IsActive");

            // Audit fields
            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.Property(t => t.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
