using Application.Common.Interfaces;

namespace Infrastructure.Services
{
    public class TenantService : ITenantService
    {
        private Guid? _tenantId;

        public Guid? TenantId => _tenantId;

        public void SetTenant(Guid tenantId)
        {
            _tenantId = tenantId;
        }
    }
}