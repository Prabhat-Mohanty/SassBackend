using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DealConfiguration : IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> builder)
        {
            builder.ToTable("Deals");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(d => d.Stage)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(d => d.Description)
                .HasColumnType("nvarchar(max)");

            builder.Property(d => d.LeadSource)
                .HasMaxLength(100);

            builder.Property(d => d.NextStep)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(d => d.Tenant)
                .WithMany()
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Company)
                .WithMany(c => c.Deals)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Contact)
                .WithMany(c => c.Deals)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Owner)
                .WithMany()
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(d => d.Activities)
                .WithOne()
                .HasForeignKey("RelatedToId")
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(d => d.TenantId)
                .HasDatabaseName("IX_Deals_TenantId");

            builder.HasIndex(d => d.Stage)
                .HasDatabaseName("IX_Deals_Stage");

            builder.HasIndex(d => d.CompanyId)
                .HasDatabaseName("IX_Deals_CompanyId");

            builder.HasIndex(d => d.ContactId)
                .HasDatabaseName("IX_Deals_ContactId");

            builder.HasIndex(d => d.OwnerId)
                .HasDatabaseName("IX_Deals_OwnerId");

            builder.Property(d => d.CreatedAt).IsRequired();
            builder.Property(d => d.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
