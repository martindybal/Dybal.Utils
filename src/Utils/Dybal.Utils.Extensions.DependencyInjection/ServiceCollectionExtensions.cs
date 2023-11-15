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
            ServiceDescriptor ToServiceDescriptor(RegisterServiceAttribute registerServiceAttribute)
            {
                var serviceType = registerServiceAttribute.As ?? implementationType;
                return new ServiceDescriptor(serviceType, implementationType, registerServiceAttribute.Lifetime);
            }

            return registerServiceAttributes.Select(ToServiceDescriptor);
        }
    }
}
