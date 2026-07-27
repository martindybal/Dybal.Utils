# Changelog

All notable changes to `Dybal.Utils.Extensions.DependencyInjection` are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.0] - 2026-07-27

### Added
- Register a single implementation under multiple service types that resolve to one shared instance. Any registration attribute (`[RegisterService]`, `[RegisterSingletonService]`, `[RegisterScopedService]`, `[RegisterTransientService]`) can now be applied more than once on the same class. Service types sharing a lifetime resolve to a single shared instance - `Singleton` across the container, `Scoped` within a scope. The concrete type stays hidden from the container unless a parameterless attribute explicitly opts it in. `Transient` keeps a new instance per resolution.

### Changed
- `RegisterServiceAttribute` and `RegisterServiceAttribute<TAs>` are now marked `AllowMultiple = true`.

## [1.0.0]

### Added
- Attribute-based service registration via `AddMarkedServicesInAssembly` and `AddMarkedServicesFromAllAssemblies`.
- Registration attributes `[RegisterService]`, `[RegisterSingletonService]`, `[RegisterScopedService]` and `[RegisterTransientService]` supporting the `Singleton`, `Scoped` and `Transient` lifetimes, registered as the concrete type or a specific interface.
