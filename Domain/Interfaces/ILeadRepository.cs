using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface ILeadRepository : IBaseRepository<Lead>
    {
        Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status, Guid tenantId);
        Task<IEnumerable<Lead>> GetByTenantAsync(Guid tenantId);
        Task<IEnumerable<Lead>> GetConvertedLeadsAsync(Guid tenantId);
        Task<IEnumerable<Lead>> GetByOwnerAsync(Guid ownerId);
        Task<IEnumerable<Lead>> SearchAsync(string searchTerm, Guid tenantId);
    }
}
