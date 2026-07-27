using Dybal.Utils.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Tests.Dybal.Utils.Extensions.DependencyInjection.Services;
using Xunit;

namespace Tests.Dybal.Utils.Extensions.DependencyInjection;

public class SharedInstanceRegistrationTests : UnitTestsBase
{
    readonly ServiceCollection services = new();
    private readonly ServiceProvider serviceProvider;

    public SharedInstanceRegistrationTests()
    {
        services.AddMarkedServicesInAssembly(typeof(SharedInstanceRegistrationTests).Assembly);
        serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void MultipleInterfaces_ResolveToSameSingletonInstance()
    {
        var a = serviceProvider.GetService<ISharedServiceA>();
        var b = serviceProvider.GetService<ISharedServiceB>();

        Assert.NotNull(a);
        Assert.NotNull(b);
        Assert.Same(a, b);
    }

    [Fact]
    public void InterfaceOnly_DoesNotRegisterConcreteType()
    {
        var concrete = serviceProvider.GetService<SharedSingletonService>();

        Assert.Null(concrete);
        Assert.DoesNotContain(services, descriptor => descriptor.ServiceType == typeof(SharedSingletonService));
    }

    [Fact]
    public void ExplicitConcreteType_SharesInstanceWithInterfaces()
    {
        var concrete = serviceProvider.GetService<SharedSingletonWithConcreteService>();
        var a = serviceProvider.GetService<ISharedConcreteServiceA>();
        var b = serviceProvider.GetService<ISharedConcreteServiceB>();

        Assert.NotNull(concrete);
        Assert.Same(concrete, a);
        Assert.Same(concrete, b);
    }

    [Fact]
    public void ScopedInterfaces_ShareInstanceWithinScope()
    {
        using var scope = serviceProvider.CreateScope();

        var a = scope.ServiceProvider.GetService<ISharedScopedServiceA>();
        var b = scope.ServiceProvider.GetService<ISharedScopedServiceB>();

        Assert.NotNull(a);
        Assert.Same(a, b);
    }

    [Fact]
    public void ScopedInterfaces_DifferInstanceAcrossScopes()
    {
        using var firstScope = serviceProvider.CreateScope();
        using var secondScope = serviceProvider.CreateScope();

        var fromFirst = firstScope.ServiceProvider.GetService<ISharedScopedServiceA>();
        var fromSecond = secondScope.ServiceProvider.GetService<ISharedScopedServiceB>();

        Assert.NotNull(fromFirst);
        Assert.NotNull(fromSecond);
        Assert.NotSame(fromFirst, fromSecond);
    }

    [Fact]
    public void TransientInterfaces_ResolveToNewInstances()
    {
        var a = serviceProvider.GetService<ISharedTransientServiceA>();
        var b = serviceProvider.GetService<ISharedTransientServiceB>();

        Assert.NotNull(a);
        Assert.NotNull(b);
        Assert.NotSame(a, b);
    }

    [Fact]
    public void ExistingSelfAndInterface_ResolveToSameSingletonInstance()
    {
        var self = serviceProvider.GetService<TestSingletonService>();
        var byInterface = serviceProvider.GetService<ITestSingletonService>();

        Assert.NotNull(self);
        Assert.Same(self, byInterface);
    }
}
