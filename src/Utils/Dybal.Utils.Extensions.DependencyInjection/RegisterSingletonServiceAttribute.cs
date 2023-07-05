using Microsoft.Extensions.DependencyInjection;

namespace Dybal.Utils.Extensions.DependencyInjection;

public class RegisterSingletonServiceAttribute : RegisterServiceAttribute
{
    public RegisterSingletonServiceAttribute(Type? @as = null)
        : base(ServiceLifetime.Singleton, @as)
    {
    }
}

public class RegisterSingletonServiceAttribute<TAs> : RegisterServiceAttribute<TAs>
{
    public RegisterSingletonServiceAttribute() : base(ServiceLifetime.Singleton)
    {
    }
}
