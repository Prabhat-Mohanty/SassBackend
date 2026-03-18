using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Tenant> Tenants { get; }
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<Company> Companies { get; }
        DbSet<Contact> Contacts { get; }
        DbSet<Lead> Leads { get; }
        DbSet<Deal> Deals { get; }
        DbSet<Activity> Activities { get; }
        DbSet<Note> Notes { get; }
        DbSet<AuditLog> AuditLogs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
