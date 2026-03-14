namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITenantRepository Tenants { get; }
        IUserRepository Users { get; }
        ICompanyRepository Companies { get; }
        IContactRepository Contacts { get; }
        ILeadRepository Leads { get; }
        IDealRepository Deals { get; }
        IActivityRepository Activities { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
