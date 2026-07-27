using Dybal.Utils.Extensions.DependencyInjection;

namespace Tests.Dybal.Utils.Extensions.DependencyInjection.Services;

public interface ISharedTransientServiceA
{
}

public interface ISharedTransientServiceB
{
}

[RegisterTransientService<ISharedTransientServiceA>]
[RegisterTransientService<ISharedTransientServiceB>]
public class SharedTransientService : ISharedTransientServiceA, ISharedTransientServiceB
{
}
