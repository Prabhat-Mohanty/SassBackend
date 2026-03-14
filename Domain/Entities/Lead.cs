using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Lead : BaseAuditableEntity, ITenantEntity
    {
        public Guid TenantId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string LeadSource { get; set; } = string.Empty; // Website, Referral, LinkedIn, etc.
        public LeadStatus Status { get; set; }
        public string Rating { get; set; } = string.Empty; // Hot, Warm, Cold
        public decimal? EstimatedValue { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid? OwnerId { get; set; }

        // Conversion tracking
        public Guid? ConvertedToContactId { get; set; }
        public Guid? ConvertedToDealId { get; set; }
        public DateTime? ConvertedAt { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public User Owner { get; set; } = null!;
        public Contact ConvertedToContact { get; set; } = null!;
        public Deal ConvertedToDeal { get; set; } = null!;

        public Lead()
        {
            Status = LeadStatus.New;
            Rating = "Warm";
        }

        public string FullName => $"{FirstName} {LastName}";
        public bool IsConverted => ConvertedAt.HasValue;
    }
}
