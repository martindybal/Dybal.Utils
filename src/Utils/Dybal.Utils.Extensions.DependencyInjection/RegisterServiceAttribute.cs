using Microsoft.Extensions.DependencyInjection;

namespace Dybal.Utils.Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Class)]
public class RegisterServiceAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; }
    public Type? As { get; }

    public RegisterServiceAttribute(ServiceLifetime lifetime, Type? @as = null)
    {
        Lifetime = lifetime;
        As = @as;
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class RegisterServiceAttribute<TAs> : RegisterServiceAttribute
{
    public RegisterServiceAttribute(ServiceLifetime lifetime) : base(lifetime, typeof(TAs))
    {
    }
}
