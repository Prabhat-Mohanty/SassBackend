using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(
            ApplicationDbContext context,
            ITenantService tenantService)
            : base(context, tenantService)
        {
        }

        public async Task<IEnumerable<Company>> GetByTenantAsync(Guid tenantId)
        {
            return await _dbSet
                .Where(c => c.TenantId == tenantId && !c.IsDeleted)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Company?> GetWithContactsAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Contacts.Where(ct => !ct.IsDeleted))
                .Include(c => c.Deals.Where(d => !d.IsDeleted))
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<IEnumerable<Company>> SearchByNameAsync(string searchTerm, Guid tenantId)
        {
            return await _dbSet
                .Where(c =>
                    c.TenantId == tenantId &&
                    c.Name.ToLower().Contains(searchTerm.ToLower()) &&
                    !c.IsDeleted)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}
