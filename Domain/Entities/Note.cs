using Domain.Common;

namespace Domain.Entities
{
    public class Note : BaseAuditableEntity, ITenantEntity
    {
        public Guid TenantId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        // Polymorphic relationship
        public string RelatedToType { get; set; } = string.Empty; // Company, Contact, Lead, Deal
        public Guid? RelatedToId { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;

        public Note()
        {
        }
    }
}
