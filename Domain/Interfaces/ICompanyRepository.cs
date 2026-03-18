using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        Task<IEnumerable<Company>> GetByTenantAsync(Guid tenantId);
        Task<Company?> GetWithContactsAsync(Guid id);
        Task<IEnumerable<Company>> SearchByNameAsync(string searchTerm, Guid tenantId);
    }
}
