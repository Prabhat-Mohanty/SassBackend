using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Industry)
                .HasMaxLength(100);

            builder.Property(c => c.Website)
                .HasMaxLength(500);

            builder.Property(c => c.Email)
                .HasMaxLength(200);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(50);

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

            builder.Property(c => c.AnnualRevenue)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Description)
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.LogoUrl)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(c => c.Tenant)
                .WithMany()
                .HasForeignKey(c => c.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Contacts)
                .WithOne(ct => ct.Company)
                .HasForeignKey(ct => ct.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Deals)
                .WithOne(d => d.Company)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(c => c.TenantId)
                .HasDatabaseName("IX_Companies_TenantId");

            builder.HasIndex(c => new { c.TenantId, c.Name })
                .HasDatabaseName("IX_Companies_TenantId_Name");

            builder.HasIndex(c => c.OwnerId)
                .HasDatabaseName("IX_Companies_OwnerId");

            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
