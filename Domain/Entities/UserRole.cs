using Domain.Common;

namespace Domain.Entities
{
    public class UserRole : BaseEntity, ITenantEntity
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;

        public UserRole()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
