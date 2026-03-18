using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        Task<IEnumerable<Contact>> GetByCompanyAsync(Guid companyId);
        Task<IEnumerable<Contact>> GetByTenantAsync(Guid tenantId);
        Task<Contact?> GetWithDealsAsync(Guid id);
        Task<IEnumerable<Contact>> SearchByNameAsync(string searchTerm, Guid tenantId);
        Task<bool> EmailExistsAsync(string email, Guid tenantId);
    }
}
