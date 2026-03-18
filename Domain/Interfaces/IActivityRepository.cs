using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IActivityRepository : IBaseRepository<Activity>
    {
        Task<IEnumerable<Activity>> GetByOwnerAsync(Guid ownerId);
        Task<IEnumerable<Activity>> GetUpcomingAsync(Guid ownerId, int days = 7);
        Task<IEnumerable<Activity>> GetOverdueAsync(Guid ownerId);
        Task<IEnumerable<Activity>> GetByTypeAsync(ActivityType type, Guid tenantId);
        Task<IEnumerable<Activity>> GetByStatusAsync(ActivityStatus status, Guid tenantId);
        Task<IEnumerable<Activity>> GetByRelatedEntityAsync(string relatedToType, Guid relatedToId);
    }
}
