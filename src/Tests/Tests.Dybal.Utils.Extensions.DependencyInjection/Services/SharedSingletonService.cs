using Dybal.Utils.Extensions.DependencyInjection;

namespace Tests.Dybal.Utils.Extensions.DependencyInjection.Services;

[RegisterSingletonService<ISharedServiceA>]
[RegisterSingletonService<ISharedServiceB>]
public class SharedSingletonService : ISharedServiceA, ISharedServiceB
{
}
