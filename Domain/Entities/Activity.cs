using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Activity : BaseAuditableEntity, ITenantEntity
    {
        public Guid TenantId { get; set; }
        public ActivityType Type { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ActivityStatus Status { get; set; }
        public Priority? Priority { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? DurationMinutes { get; set; }
        public string Location { get; set; } = string.Empty;

        // Polymorphic relationship - can relate to different entities
        public string RelatedToType { get; set; } = string.Empty; // Company, Contact, Lead, Deal
        public Guid? RelatedToId { get; set; }

        public Guid? OwnerId { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public User Owner { get; set; } = null!;

        public Activity()
        {
            Status = ActivityStatus.Planned;
            Type = ActivityType.Task;
        }

        public bool IsOverdue => ScheduledAt.HasValue
            && ScheduledAt.Value < DateTime.UtcNow
            && Status != ActivityStatus.Completed;
    }
}
