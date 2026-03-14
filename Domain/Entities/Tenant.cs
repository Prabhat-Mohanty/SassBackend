using Domain.Enums;

namespace Domain.Entities
{
    public class Tenant : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Subdomain { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public SubscriptionTier SubscriptionTier { get; set; }
        public DateTime? SubscriptionExpiresAt { get; set; }
        public int MaxUsers { get; set; }

        public Tenant()
        {
            IsActive = true;
            MaxUsers = 5;
            SubscriptionTier = SubscriptionTier.Free;
        }
    }
}
