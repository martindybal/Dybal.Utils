using Dybal.Utils.Extensions.DependencyInjection;

namespace Tests.Dybal.Utils.Extensions.DependencyInjection.Services;

public interface ISharedScopedServiceA
{
}

public interface ISharedScopedServiceB
{
}

[RegisterScopedService<ISharedScopedServiceA>]
[RegisterScopedService<ISharedScopedServiceB>]
public class SharedScopedService : ISharedScopedServiceA, ISharedScopedServiceB
{
}
