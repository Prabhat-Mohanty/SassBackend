using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(
            ApplicationDbContext context,
            ITenantService tenantService)
            : base(context, tenantService)
        {
        }

        public async Task<IEnumerable<Contact>> GetByCompanyAsync(Guid companyId)
        {
            return await _dbSet
                .Where(c => c.CompanyId == companyId && !c.IsDeleted)
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetByTenantAsync(Guid tenantId)
        {
            return await _dbSet
                .Include(c => c.Company)
                .Where(c => c.TenantId == tenantId && !c.IsDeleted)
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToListAsync();
        }

        public async Task<Contact?> GetWithDealsAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Company)
                .Include(c => c.Deals.Where(d => !d.IsDeleted))
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<IEnumerable<Contact>> SearchByNameAsync(string searchTerm, Guid tenantId)
        {
            var lowerSearchTerm = searchTerm.ToLower();

            return await _dbSet
                .Include(c => c.Company)
                .Where(c =>
                    c.TenantId == tenantId &&
                    (c.FirstName.ToLower().Contains(lowerSearchTerm) ||
                     c.LastName.ToLower().Contains(lowerSearchTerm) ||
                     c.Email.ToLower().Contains(lowerSearchTerm)) &&
                    !c.IsDeleted)
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, Guid tenantId)
        {
            return await _dbSet
                .AnyAsync(c =>
                    c.Email.ToLower() == email.ToLower() &&
                    c.TenantId == tenantId &&
                    !c.IsDeleted);
        }
    }
}
