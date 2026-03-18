using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IDealRepository : IBaseRepository<Deal>
    {
        Task<IEnumerable<Deal>> GetByStageAsync(DealStage stage, Guid tenantId);
        Task<IEnumerable<Deal>> GetByTenantAsync(Guid tenantId);
        Task<IEnumerable<Deal>> GetByCompanyAsync(Guid companyId);
        Task<IEnumerable<Deal>> GetByContactAsync(Guid contactId);
        Task<Deal?> GetWithActivitiesAsync(Guid id);
        Task<decimal> GetTotalValueByStageAsync(DealStage stage, Guid tenantId);
        Task<IEnumerable<Deal>> GetClosingThisMonthAsync(Guid tenantId);
    }
}
