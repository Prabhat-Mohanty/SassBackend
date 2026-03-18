using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(
            ApplicationDbContext context,
            ITenantService tenantService)
            : base(context, tenantService)
        {
        }

        public async Task<IEnumerable<Activity>> GetByOwnerAsync(Guid ownerId)
        {
            return await _dbSet
                .Where(a => a.OwnerId == ownerId && !a.IsDeleted)
                .OrderBy(a => a.ScheduledAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetUpcomingAsync(Guid ownerId, int days = 7)
        {
            var now = DateTime.UtcNow;
            var futureDate = now.AddDays(days);

            return await _dbSet
                .Where(a =>
                    a.OwnerId == ownerId &&
                    a.ScheduledAt >= now &&
                    a.ScheduledAt <= futureDate &&
                    a.Status != ActivityStatus.Completed &&
                    a.Status != ActivityStatus.Cancelled &&
                    !a.IsDeleted)
                .OrderBy(a => a.ScheduledAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetOverdueAsync(Guid ownerId)
        {
            var now = DateTime.UtcNow;

            return await _dbSet
                .Where(a =>
                    a.OwnerId == ownerId &&
                    a.ScheduledAt < now &&
                    a.Status != ActivityStatus.Completed &&
                    a.Status != ActivityStatus.Cancelled &&
                    !a.IsDeleted)
                .OrderBy(a => a.ScheduledAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetByTypeAsync(ActivityType type, Guid tenantId)
        {
            return await _dbSet
                .Include(a => a.Owner)
                .Where(a =>
                    a.Type == type &&
                    a.TenantId == tenantId &&
                    !a.IsDeleted)
                .OrderByDescending(a => a.ScheduledAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetByStatusAsync(ActivityStatus status, Guid tenantId)
        {
            return await _dbSet
                .Include(a => a.Owner)
                .Where(a =>
                    a.Status == status &&
                    a.TenantId == tenantId &&
                    !a.IsDeleted)
                .OrderByDescending(a => a.ScheduledAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetByRelatedEntityAsync(string relatedToType, Guid relatedToId)
        {
            return await _dbSet
                .Include(a => a.Owner)
                .Where(a =>
                    a.RelatedToType == relatedToType &&
                    a.RelatedToId == relatedToId &&
                    !a.IsDeleted)
                .OrderByDescending(a => a.ScheduledAt)
                .ToListAsync();
        }
    }
}
