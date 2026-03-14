using Domain.Common;
using Domain.Enums;
using System.Diagnostics;

namespace Domain.Entities
{
    public class Deal : BaseAuditableEntity, ITenantEntity
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? CompanyId { get; set; }
        public Guid? ContactId { get; set; }
        public decimal Amount { get; set; }
        public DealStage Stage { get; set; }
        public int? Probability { get; set; } // 0-100
        public DateTime? ExpectedCloseDate { get; set; }
        public DateTime? ActualCloseDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string LeadSource { get; set; } = string.Empty;
        public string NextStep { get; set; } = string.Empty;
        public Guid? OwnerId { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public Company Company { get; set; } = null!;
        public Contact Contact { get; set; } = null!;
        public User Owner { get; set; } = null!;
        public ICollection<Activity> Activities { get; set; }

        public Deal()
        {
            Stage = DealStage.Prospecting;
            Probability = 10;
            Activities = new HashSet<Activity>();
        }

        public bool IsClosed => Stage == DealStage.ClosedWon || Stage == DealStage.ClosedLost;
        public bool IsWon => Stage == DealStage.ClosedWon;
    }
}
