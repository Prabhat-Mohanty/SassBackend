using Domain.Common;

namespace Domain.Entities
{
    public class Contact : BaseAuditableEntity, ITenantEntity
    {
        public Guid TenantId { get; set; }
        public Guid? CompanyId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public string Twitter { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public Guid? OwnerId { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public Company Company { get; set; } = null!;
        public User Owner { get; set; } = null!;
        public ICollection<Deal> Deals { get; set; }

        public Contact()
        {
            Deals = new HashSet<Deal>();
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}
