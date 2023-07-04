# Dybal.Utils.Extensions.DependencyInjection

This library provides a straightforward and seamless way to register all of your application's services in a dotnet project using attributes. With this library, you can significantly reduce the complexity and verbosity of your `Startup` class and make your service registration more clean and easy to understand.

## Features
- Provides attributes for marking services for registration.
- Automatic service registration using reflection.
- Support for all service lifetimes: `Singleton`, `Scoped`, and `Transient`.
- Support register a service with a specific interface or as self.

## Installation

The library can be installed via NuGet.
```
dotnet add package Dybal.Utils.Extensions.DependencyInjection
```

## Usage

First, you need to call the `AddMarkedServicesInAssembly` extension method in your `Startup` class' `ConfigureServices` method.

```C#
public void ConfigureServices(IServiceCollection services)
{
    // ...
    services.AddMarkedServicesInAssembly(Assembly.GetExecutingAssembly());
    // ...
}
```

This method will scan the assembly for classes that are decorated with any of the registration attributes and add them to the DI container with the specified lifetime.

### Add services from multiple assemblies

```C#
public void ConfigureServices(IServiceCollection services)
{
    // ...
    
    services.AddMarkedServicesFromAllAssemblies();
    // or
    services.AddMarkedServicesFromAllAssemblies("MyProject.");

    // ...
}
```

This method loops over all loaded assemblies, filtering them by the provided `assemblyNamePrefix` parameter, and registers all services decorated with the registration attributes from those assemblies. Parameter `assemblyNamePrefix` is optional all assembiels are scanned if it is not provided.

### Register a service with its concrete type

```C#
[RegisterService(ServiceLifetime.Scoped)]
public class SomeScopedService
{
    // Implementation goes here...
}
```

With `RegisterService` attribute, the service is registered with its concrete type.

### Register a service with a specific interface

```C#
[RegisterService<IScopedService>(ServiceLifetime.Scoped)]
public class SomeScopedService : IScopedService
{
    // Implementation goes here...
}
```

`RegisterService` attribute can also be used to register a service with a specific interface.

### Shortcut attributes for specific lifetimes

```C#
[RegisterSingletonService]
public class SomeSingletonService
{
    // Implementation goes here...
}

[RegisterTransientService]
public class SomeTransientService
{
    // Implementation goes here...
}

[RegisterScopedService]
public class SomeScopedService
{
    // Implementation goes here...
}
```

These attributes register the service with their respective lifetimes. They can also be used with interfaces.