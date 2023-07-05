using Dybal.Utils.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Dybal.Utils.Extensions.DependencyInjection.Services;

[RegisterScopedService]
[RegisterScopedService<ITestScopedService>]
public class TestScopedService : ITestScopedService
{
}