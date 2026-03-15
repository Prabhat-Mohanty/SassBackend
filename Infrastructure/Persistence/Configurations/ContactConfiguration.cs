using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .HasMaxLength(200);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(c => c.MobileNumber)
                .HasMaxLength(50);

            builder.Property(c => c.JobTitle)
                .HasMaxLength(100);

            builder.Property(c => c.Department)
                .HasMaxLength(100);

            builder.Property(c => c.Address)
                .HasMaxLength(500);

            builder.Property(c => c.City)
                .HasMaxLength(100);

            builder.Property(c => c.State)
                .HasMaxLength(100);

            builder.Property(c => c.Country)
                .HasMaxLength(100);

            builder.Property(c => c.PostalCode)
                .HasMaxLength(20);

            builder.Property(c => c.LinkedIn)
                .HasMaxLength(500);

            builder.Property(c => c.Twitter)
                .HasMaxLength(500);

            builder.Property(c => c.Notes)
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.ProfilePictureUrl)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(c => c.Tenant)
                .WithMany()
                .HasForeignKey(c => c.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Company)
                .WithMany(co => co.Contacts)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Deals)
                .WithOne(d => d.Contact)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(c => c.TenantId)
                .HasDatabaseName("IX_Contacts_TenantId");

            builder.HasIndex(c => c.CompanyId)
                .HasDatabaseName("IX_Contacts_CompanyId");

            builder.HasIndex(c => new { c.TenantId, c.Email })
                .HasDatabaseName("IX_Contacts_TenantId_Email");

            builder.HasIndex(c => c.OwnerId)
                .HasDatabaseName("IX_Contacts_OwnerId");

            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
