using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ITenantService _tenantService;

        public BaseRepository(
            ApplicationDbContext context,
            ITenantService tenantService)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _tenantService = tenantService;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet
                .Where(e => !((BaseAuditableEntity)(object)e).IsDeleted)
                .ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .Where(e => !((BaseAuditableEntity)(object)e).IsDeleted)
                .ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                // Soft delete
                if (entity is BaseAuditableEntity auditableEntity)
                {
                    auditableEntity.IsDeleted = true;
                    auditableEntity.DeletedAt = DateTime.UtcNow;
                    _dbSet.Update(entity);
                }
                else
                {
                    _dbSet.Remove(entity);
                }
            }
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet
                .AnyAsync(e => e.Id == id);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbSet
                .Where(e => !((BaseAuditableEntity)(object)e).IsDeleted)
                .CountAsync();
        }
    }
}
