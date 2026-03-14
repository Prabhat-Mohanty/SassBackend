using Domain.Common;

namespace Domain.Entities
{
    public class AuditLog : BaseEntity, ITenantEntity
    {
        public Guid TenantId { get; set; }
        public Guid? UserId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public string Action { get; set; } = string.Empty; // Created, Updated, Deleted
        public string OldValues { get; set; } = string.Empty; // JSON
        public string NewValues { get; set; } = string.Empty; // JSON
        public DateTime Timestamp { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public User User { get; set; } = null!;

        public AuditLog()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
