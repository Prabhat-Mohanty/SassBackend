using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class LeadConfiguration : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.ToTable("Leads");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.Email)
                .HasMaxLength(200);

            builder.Property(l => l.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(l => l.CompanyName)
                .HasMaxLength(200);

            builder.Property(l => l.JobTitle)
                .HasMaxLength(100);

            builder.Property(l => l.LeadSource)
                .HasMaxLength(100);

            builder.Property(l => l.Status)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(l => l.Rating)
                .HasMaxLength(50);

            builder.Property(l => l.EstimatedValue)
                .HasColumnType("decimal(18,2)");

            builder.Property(l => l.Description)
                .HasColumnType("nvarchar(max)");

            // Relationships
            builder.HasOne(l => l.Tenant)
                .WithMany()
                .HasForeignKey(l => l.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Owner)
                .WithMany()
                .HasForeignKey(l => l.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.ConvertedToContact)
                .WithMany()
                .HasForeignKey(l => l.ConvertedToContactId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.ConvertedToDeal)
                .WithMany()
                .HasForeignKey(l => l.ConvertedToDealId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(l => l.TenantId)
                .HasDatabaseName("IX_Leads_TenantId");

            builder.HasIndex(l => l.Status)
                .HasDatabaseName("IX_Leads_Status");

            builder.HasIndex(l => l.OwnerId)
                .HasDatabaseName("IX_Leads_OwnerId");

            builder.HasIndex(l => new { l.TenantId, l.Email })
                .HasDatabaseName("IX_Leads_TenantId_Email");

            builder.Property(l => l.CreatedAt).IsRequired();
            builder.Property(l => l.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
