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
            // Attributes are grouped by lifetime so that every service type sharing a lifetime resolves to a single
            // shared instance. Different lifetimes form independent registrations (they cannot share an instance).
            foreach (var group in registerServiceAttributes.GroupBy(attribute => attribute.Lifetime))
            {
                var lifetime = group.Key;
                var serviceTypes = group.Select(attribute => attribute.As ?? implementationType).Distinct().ToList();

                // Transient never shares an instance - each resolution produces a new one - so register every
                // service type directly against the implementation type.
                if (lifetime == ServiceLifetime.Transient)
                {
                    foreach (var serviceType in serviceTypes)
                    {
                        yield return new ServiceDescriptor(serviceType, implementationType, lifetime);
                    }

                    continue;
                }

                // Pick the owner deterministically (reflection does not guarantee attribute order): the concrete type
                // when it is explicitly declared, otherwise the first declared service type ordered by name. The owner
                // instantiates the implementation; every other service type forwards to it so they share one instance.
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
