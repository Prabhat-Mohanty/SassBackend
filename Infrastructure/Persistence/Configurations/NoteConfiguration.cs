using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Notes");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title)
                .HasMaxLength(200);

            builder.Property(n => n.Content)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(n => n.RelatedToType)
                .HasMaxLength(50);

            // Relationships
            builder.HasOne(n => n.Tenant)
                .WithMany()
                .HasForeignKey(n => n.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(n => n.TenantId)
                .HasDatabaseName("IX_Notes_TenantId");

            builder.HasIndex(n => new { n.RelatedToType, n.RelatedToId })
                .HasDatabaseName("IX_Notes_RelatedTo");

            builder.Property(n => n.CreatedAt).IsRequired();
            builder.Property(n => n.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
