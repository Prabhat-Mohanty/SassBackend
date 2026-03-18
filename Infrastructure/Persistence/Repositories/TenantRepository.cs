using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TenantRepository : BaseRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(
            ApplicationDbContext context,
            ITenantService tenantService)
            : base(context, tenantService)
        {
        }

        public async Task<Tenant?> GetBySubdomainAsync(string subdomain)
        {
            return await _dbSet
                .FirstOrDefaultAsync(t =>
                    t.Subdomain.ToLower() == subdomain.ToLower() &&
                    !t.IsDeleted);
        }

        public async Task<bool> SubdomainExistsAsync(string subdomain)
        {
            return await _dbSet
                .AnyAsync(t =>
                    t.Subdomain.ToLower() == subdomain.ToLower() &&
                    !t.IsDeleted);
        }
    }
}
