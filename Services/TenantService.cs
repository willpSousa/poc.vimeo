using Poc.Vimeo.Configuration;

namespace Poc.Vimeo.Services;

public class TenantService
{
    public TenantConfiguration GetTenant() =>
        ServiceLocator.Get<TenantConfiguration>()!;
}
