using Dybal.Utils.Extensions.DependencyInjection;

namespace Tests.Dybal.Utils.Extensions.DependencyInjection.Services;

[RegisterSingletonService]
[RegisterSingletonService<ITestSingletonService>]
public class TestSingletonService : ITestSingletonService
{
}