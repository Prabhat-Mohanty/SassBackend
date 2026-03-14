using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class User : BaseAuditableEntity, ITenantEntity
    {
        public Guid TenantId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public ICollection<UserRole> UserRoles { get; set; }

        public User()
        {
            Status = UserStatus.Active;
            EmailConfirmed = false;
            TwoFactorEnabled = false;
            UserRoles = new HashSet<UserRole>();
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}
