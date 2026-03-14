using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITenantRepository : IBaseRepository<Tenant>
    {
        Task<Tenant> GetBySubdomainAsync(string subdomain);
        Task<bool> SubdomainExistsAsync(string subdomain);
    }
}
