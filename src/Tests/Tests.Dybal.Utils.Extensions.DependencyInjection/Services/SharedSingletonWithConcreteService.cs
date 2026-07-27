using Dybal.Utils.Extensions.DependencyInjection;

namespace Tests.Dybal.Utils.Extensions.DependencyInjection.Services;

public interface ISharedConcreteServiceA
{
}

public interface ISharedConcreteServiceB
{
}

[RegisterSingletonService]
[RegisterSingletonService<ISharedConcreteServiceA>]
[RegisterSingletonService<ISharedConcreteServiceB>]
public class SharedSingletonWithConcreteService : ISharedConcreteServiceA, ISharedConcreteServiceB
{
}
