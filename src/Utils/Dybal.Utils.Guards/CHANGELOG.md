# Changelog

## 1.1.0

### Added

- Finite floating point numbers, UTC `DateTime` values and absolute URI guards.
- Unique collections and keys, allowed values, character alphabets, optional nonblank text, integer divisibility and at least one nonblank string.
- `Satisfies` for predicates that validate the original argument.
- Enumeration of guarded collections in `foreach` and collection expressions.
- `EnumDefined`, including a nullable overload that rejects null; `True`, `False`, `InRange` and `MatchesPattern`, previously present in source but absent from the 1.0.1 package.
- Bundled analyzers for computed conditions, boolean equality assertions and collection or string projections with dedicated guards.

### Fixed

- `EqualTo` and `NotEqualTo` support enums and nullable values through `EqualityComparer<T>.Default`.
- Collection `NotNullOrEmpty` can infer the element type.
- Packing the Guards project builds and bundles the analyzer project automatically.
