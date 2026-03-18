using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class LeadRepository : BaseRepository<Lead>, ILeadRepository
    {
        public LeadRepository(
            ApplicationDbContext context,
            ITenantService tenantService)
            : base(context, tenantService)
        {
        }

        public async Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status, Guid tenantId)
        {
            return await _dbSet
                .Where(l =>
                    l.Status == status &&
                    l.TenantId == tenantId &&
                    !l.IsDeleted)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lead>> GetByTenantAsync(Guid tenantId)
        {
            return await _dbSet
                .Include(l => l.Owner)
                .Where(l => l.TenantId == tenantId && !l.IsDeleted)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lead>> GetConvertedLeadsAsync(Guid tenantId)
        {
            return await _dbSet
                .Include(l => l.ConvertedToContact)
                .Include(l => l.ConvertedToDeal)
                .Where(l =>
                    l.TenantId == tenantId &&
                    l.ConvertedAt != null &&
                    !l.IsDeleted)
                .OrderByDescending(l => l.ConvertedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lead>> GetByOwnerAsync(Guid ownerId)
        {
            return await _dbSet
                .Where(l => l.OwnerId == ownerId && !l.IsDeleted)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lead>> SearchAsync(string searchTerm, Guid tenantId)
        {
            var lowerSearchTerm = searchTerm.ToLower();

            return await _dbSet
                .Where(l =>
                    l.TenantId == tenantId &&
                    (l.FirstName.ToLower().Contains(lowerSearchTerm) ||
                     l.LastName.ToLower().Contains(lowerSearchTerm) ||
                     l.Email.ToLower().Contains(lowerSearchTerm) ||
                     l.CompanyName.ToLower().Contains(lowerSearchTerm)) &&
                    !l.IsDeleted)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }
    }
}
