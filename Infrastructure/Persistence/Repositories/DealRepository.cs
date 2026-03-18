using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class DealRepository : BaseRepository<Deal>, IDealRepository
    {
        public DealRepository(
            ApplicationDbContext context,
            ITenantService tenantService)
            : base(context, tenantService)
        {
        }

        public async Task<IEnumerable<Deal>> GetByStageAsync(DealStage stage, Guid tenantId)
        {
            return await _dbSet
                .Include(d => d.Company)
                .Include(d => d.Contact)
                .Where(d =>
                    d.Stage == stage &&
                    d.TenantId == tenantId &&
                    !d.IsDeleted)
                .OrderByDescending(d => d.Amount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Deal>> GetByTenantAsync(Guid tenantId)
        {
            return await _dbSet
                .Include(d => d.Company)
                .Include(d => d.Contact)
                .Include(d => d.Owner)
                .Where(d => d.TenantId == tenantId && !d.IsDeleted)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Deal>> GetByCompanyAsync(Guid companyId)
        {
            return await _dbSet
                .Include(d => d.Contact)
                .Include(d => d.Owner)
                .Where(d => d.CompanyId == companyId && !d.IsDeleted)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Deal>> GetByContactAsync(Guid contactId)
        {
            return await _dbSet
                .Include(d => d.Company)
                .Include(d => d.Owner)
                .Where(d => d.ContactId == contactId && !d.IsDeleted)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        public async Task<Deal?> GetWithActivitiesAsync(Guid id)
        {
            return await _dbSet
                .Include(d => d.Company)
                .Include(d => d.Contact)
                .Include(d => d.Owner)
                .Include(d => d.Activities.Where(a => !a.IsDeleted))
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }

        public async Task<decimal> GetTotalValueByStageAsync(DealStage stage, Guid tenantId)
        {
            return await _dbSet
                .Where(d =>
                    d.Stage == stage &&
                    d.TenantId == tenantId &&
                    !d.IsDeleted)
                .SumAsync(d => d.Amount);
        }

        public async Task<IEnumerable<Deal>> GetClosingThisMonthAsync(Guid tenantId)
        {
            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            return await _dbSet
                .Include(d => d.Company)
                .Include(d => d.Contact)
                .Include(d => d.Owner)
                .Where(d =>
                    d.TenantId == tenantId &&
                    d.ExpectedCloseDate >= startOfMonth &&
                    d.ExpectedCloseDate <= endOfMonth &&
                    d.Stage != DealStage.ClosedWon &&
                    d.Stage != DealStage.ClosedLost &&
                    !d.IsDeleted)
                .OrderBy(d => d.ExpectedCloseDate)
                .ToListAsync();
        }
    }
}
