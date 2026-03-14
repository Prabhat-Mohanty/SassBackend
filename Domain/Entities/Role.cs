using Domain.Common;

namespace Domain.Entities
{

    public class Role : BaseAuditableEntity, ITenantEntity
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsSystemRole { get; set; }
        public string Permissions { get; set; } = string.Empty; // JSON array

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            IsSystemRole = false;
            UserRoles = new HashSet<UserRole>();
        }
    }
}
