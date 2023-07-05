using Microsoft.Extensions.DependencyInjection;

namespace Dybal.Utils.Extensions.DependencyInjection;

public class RegisterScopedServiceAttribute : RegisterServiceAttribute
{
    public RegisterScopedServiceAttribute(Type? @as = null)
        : base(ServiceLifetime.Scoped, @as)
    {
    }
}

public class RegisterScopedServiceAttribute<TAs> : RegisterServiceAttribute<TAs>
{
    public RegisterScopedServiceAttribute() : base(ServiceLifetime.Scoped)
    {
    }
}
