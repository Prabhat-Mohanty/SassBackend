using Domain.Common;

namespace Domain.Entities
{
    public class Company : BaseAuditableEntity, ITenantEntity
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public int? EmployeeCount { get; set; }
        public decimal? AnnualRevenue { get; set; }
        public string Description { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public Guid? OwnerId { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
        public User Owner { get; set; } = null!;
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Deal> Deals { get; set; }

        public Company()
        {
            Contacts = new HashSet<Contact>();
            Deals = new HashSet<Deal>();
        }
    }
}
