using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(
            ApplicationDbContext context,
            ITenantService tenantService)
            : base(context, tenantService)
        {
        }

        public async Task<User?> GetByEmailAsync(string email, Guid tenantId)
        {
            return await _dbSet
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u =>
                    u.Email.ToLower() == email.ToLower() &&
                    u.TenantId == tenantId &&
                    !u.IsDeleted);
        }

        public async Task<IEnumerable<User>> GetByTenantAsync(Guid tenantId)
        {
            return await _dbSet
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.TenantId == tenantId && !u.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, Guid tenantId)
        {
            return await _dbSet
                .AnyAsync(u =>
                    u.Email.ToLower() == email.ToLower() &&
                    u.TenantId == tenantId &&
                    !u.IsDeleted);
        }
    }
}
