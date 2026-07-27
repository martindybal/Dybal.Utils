using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Dybal.Utils.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMarkedServicesFromAllAssemblies(this IServiceCollection services, string assemblyNamePrefix = "")
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.FullName?.StartsWith(assemblyNamePrefix) == true))
        {
            services.AddMarkedServicesInAssembly(assembly);
        }

        return services;
    }

    public static IServiceCollection AddMarkedServicesInAssembly(this IServiceCollection services, Assembly assembly)
    {
        foreach (var descriptor in GetServiceDescriptors(assembly))
        {
            services.Add(descriptor);
        }

        return services;
    }

    private static IEnumerable<ServiceDescriptor> GetServiceDescriptors(Assembly assembly)
    {
        return assembly.GetTypes().SelectMany(type => ToServiceDescriptors(type, type.GetCustomAttributes<RegisterServiceAttribute>()));

        static IEnumerable<ServiceDescriptor> ToServiceDescriptors(Type implementationType, IEnumerable<RegisterServiceAttribute> registerServiceAttributes)
        {
            foreach (var group in registerServiceAttributes.GroupBy(attribute => attribute.Lifetime))
            {
                var lifetime = group.Key;
                var serviceTypes = group.Select(attribute => attribute.As ?? implementationType).Distinct().ToList();

                if (lifetime == ServiceLifetime.Transient)
                {
                    foreach (var serviceType in serviceTypes)
                    {
                        yield return new ServiceDescriptor(serviceType, implementationType, lifetime);
                    }

                    continue;
                }

                var owner = serviceTypes.Contains(implementationType)
                    ? implementationType
                    : serviceTypes.OrderBy(serviceType => serviceType.FullName, StringComparer.Ordinal).First();

                yield return new ServiceDescriptor(owner, implementationType, lifetime);

                foreach (var serviceType in serviceTypes.Where(serviceType => serviceType != owner))
                {
                    yield return new ServiceDescriptor(serviceType, serviceProvider => serviceProvider.GetRequiredService(owner), lifetime);
                }
            }
        }
    }
}
