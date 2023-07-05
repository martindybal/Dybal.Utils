using Microsoft.Extensions.DependencyInjection;

namespace Dybal.Utils.Extensions.DependencyInjection;

public class RegisterTransientServiceAttribute : RegisterServiceAttribute
{
    public RegisterTransientServiceAttribute(Type? @as = null)
        : base(ServiceLifetime.Transient, @as)
    {
    }
}

public class RegisterTransientServiceAttribute<TAs> : RegisterServiceAttribute<TAs>
{
    public RegisterTransientServiceAttribute() : base(ServiceLifetime.Transient)
    {
    }
}
