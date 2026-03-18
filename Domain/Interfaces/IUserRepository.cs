using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, Guid tenantId);
        Task<IEnumerable<User>> GetByTenantAsync(Guid tenantId);
        Task<bool> EmailExistsAsync(string email, Guid tenantId);
    }
}
