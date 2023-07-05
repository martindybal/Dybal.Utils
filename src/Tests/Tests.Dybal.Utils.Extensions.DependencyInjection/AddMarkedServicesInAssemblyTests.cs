using Dybal.Utils.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Tests.Dybal.Utils.Extensions.DependencyInjection.Services;
using Xunit;

namespace Tests.Dybal.Utils.Extensions.DependencyInjection;

public class AddMarkedServicesInAssemblyTests : UnitTestsBase
{
    readonly ServiceCollection services = new();
    private readonly ServiceProvider serviceProvider;

    public AddMarkedServicesInAssemblyTests()
    {
        services.AddMarkedServicesInAssembly(typeof(AddMarkedServicesInAssemblyTests).Assembly);
        serviceProvider = services.BuildServiceProvider();
    }

    [Theory]
    //register as self
    [InlineData(typeof(TestScopedService))]
    [InlineData(typeof(TestSingletonService))]
    [InlineData(typeof(TestTransientService))]
    //register as interfaces
    [InlineData(typeof(ITestScopedService))]
    [InlineData(typeof(ITestSingletonService))]
    [InlineData(typeof(ITestTransientService))]
    public void RegisterServiceAsSelf(Type serviceType)
    {
        var service = serviceProvider.GetService(serviceType);

        Assert.NotNull(service);
    }
    
    [Theory]
    [InlineData(typeof(TestScopedService), ServiceLifetime.Scoped)]
    [InlineData(typeof(TestSingletonService), ServiceLifetime.Singleton)]
    [InlineData(typeof(TestTransientService), ServiceLifetime.Transient)]
    [InlineData(typeof(ITestScopedService), ServiceLifetime.Scoped)]
    [InlineData(typeof(ITestSingletonService), ServiceLifetime.Singleton)]
    [InlineData(typeof(ITestTransientService), ServiceLifetime.Transient)]
    public void CheckCorrectServiceLifetime(Type serviceType, ServiceLifetime expectedLifetime)
    {
        var serviceDescriptor = services.Single(x => x.ServiceType == serviceType);

        Assert.Equal(expectedLifetime, serviceDescriptor.Lifetime);
    }
}