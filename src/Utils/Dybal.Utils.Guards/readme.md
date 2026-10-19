# Dybal.Utils.Guards
The primary purpose of Dybal.Utils.Guards is to simplify writing input data checks to the object.

# Make your code readable as much as you can
```C#
Foo(Guid id, string bar)
{
    if(id == Guid.Empty)
    {
        throw new ArgumentException("Value cannot be an empty GUID.", nameof(id));
    }

    if(string.NotNullOrWhiteSpace(bar))
    {
        throw new ArgumentException("Value cannot be null or white space string.", nameof(bar));
    }
    if (bar < 5)
    {
        throw new ArgumentException($"The length of '{nameof(bar)}' must be 5 characters or more. Parameter {bar.Length} has characters.", nameof(bar));
    }
    if (bar > 20)
    {
        throw new ArgumentException($"The length of '{nameof(bar)}' must be 20 characters or fewer. Parameter {bar.Length} has characters.", nameof(bar));
    }

    Id = id;
    Bar = bar;
}
```

### Less boilerplate and more clarity to your code
```C#
Foo(Guid id, string bar)
{
    Id = Guard.Argument(id).NotDefault();
    Bar = Guard.Argument(bar).NotNullOrWhiteSpace().MinLength(5).MaxLength(20);
}
```

If you like shortcodes, you can also use this.
```C#
using static Dybal.Utils.Guards.GuardProvider;

...

Foo(Guid? id, string bar)
{
    Id = Guard(id).NotDefault();
    Bar = Guard(bar).NotNullOrWhiteSpace().MinLength(5).MaxLength(20);
}
```

Or an Extension method from `Dybal.Utils.Guards.ObjectExtensions` package.
```C#
Foo(Guid? id, string bar)
{
    Id = id.Guard().NotDefault();
    Bar = bar.Guard().NotNullOrWhiteSpace().MinLength(5).MaxLength(20);
}
```

## Validation helpers

Always pass the value being validated to `Guard.Argument`. Use a dedicated guard, or `Satisfies` for a rule specific to your application:

```C#
Guard.Argument(accuracy).Finite().GreaterThanOrEqualTo(0);
Guard.Argument(state).EnumDefined();
Guard.Argument(timestamp).Utc();
Guard.Argument(callbackUri).Absolute();
Guard.Argument(connectors).NotNull().UniqueBy(connector => connector.Key, StringComparer.Ordinal);
Guard.Argument(status).OneOf(allowedStatuses);
Guard.Argument(optionalLabel).NotWhiteSpaceIfNotNull();
Guard.Argument(configuration).NotNull().Satisfies(value => value.Start < value.End);
```

| Guard | Behavior |
| --- | --- |
| `Finite` | Rejects NaN and both infinities for `double` and `float`. Nullable overloads allow null and preserve the nullable type. |
| `Utc` | Requires `DateTimeKind.Utc`; does not convert the timestamp. Use `NotDefault` separately when needed. |
| `Absolute` | Rejects null and relative URI values; does not restrict the URI scheme. |
| `Unique`, `UniqueBy` | Reject duplicate items or keys, with an optional equality comparer. Enumerate the sequence once and keep its original reference. |
| `OneOf` | Requires membership in the allowed values, with an optional equality comparer. |
| `NotWhiteSpaceIfNotNull` | Allows null, rejects empty and whitespace strings. |
| `ContainsOnly` | Requires every UTF-16 `char` to belong to the supplied alphabet. Empty text is allowed. |
| `DoesNotContainDuplicateCharacters` | Rejects repeated UTF-16 `char` values. Empty text is allowed. |
| `DivisibleBy` | Requires an integer to be divisible by a nonzero divisor. |
| `Satisfies` | Runs the predicate against the original value and retains its parameter name and exception override. |

`Guard.Arguments(text, attachmentUrl).AtLeastOneIsNotNullOrWhiteSpace()` requires at least one nonblank string. All supplied values must be strings or null.

`EqualTo` and `NotEqualTo` use `EqualityComparer<T>.Default`, including for enums and nullable values. Equality does not prove that an enum value is defined.

Collection guard results support enumeration directly:

```C#
List<Item> validatedItems = [.. Guard.Argument(items).NotNull().DoesNotContainNull()];
```

These guards validate sequences without materializing them. A later enumeration observes the source again, so a single-use iterator should first be materialized by the caller.

## Common mistakes
### ❌ Don't evaluate the check yourself and guard the result
Spotted in the wild, on an enum parameter:

```C#
void SetState(State state)
{
    Guard.Argument(Enum.IsDefined(state)).EqualTo(true);
}
```

It compiles, and it does throw for an invalid `state`, so it looks like it works. It doesn't - this is what the caller gets:

```
Value of parameter 'Enum.IsDefined(state)' (False) must be equal to value of parameter 'true' (True). (Parameter 'Enum.IsDefined(state)')
```

Three things went wrong, and all of them have the same root cause - **the guard never saw `state`, it only saw a `bool`**.

1. **`ParamName` describes the condition.** `Guard.Argument` fills the argument name from `[CallerArgumentExpression]`, capturing the expression `Enum.IsDefined(state)`. An explicit `nameof(state)` can fix the name, but cannot recover the original value.
2. **The offending value is lost.** By the time the guard runs, `state` has already collapsed into `false`. The message can say *that* something was invalid, but never *which* state was passed - so the exception is useless for diagnosing a production bug.
3. **The intent is lost.** `EqualTo(true)` documents the mechanics of the check, not the rule being enforced.

### ✅ Pass the argument itself and let the guard do the check

```C#
void SetState(State state)
{
    Guard.Argument(state).EnumDefined();
}
```

```
Value of parameter 'state' (7) must be defined in enum 'State'. (Parameter 'state')
```

Correct name, the actual invalid value, and the rule that was violated. `EnumDefined` also works on `State?`, where it rejects `null` too and hands back a non-nullable `State`:

```C#
void SetState(State? state)
{
    State value = Guard.Argument(state).EnumDefined();
}
```

### The rule generalises beyond enums
**Whatever you write inside `Guard.Argument(...)` becomes the reported parameter name.** Pass the argument, never an expression computed from it:

```C#
Guard.Argument(bar.Length).GreaterThan(5);   // ❌ ParamName is "bar.Length"
Guard.Argument(bar).MinLength(6);            // ✅ Same strict lower bound, original argument
```

If no guard fits your rule, use `Guard.Argument(value).Satisfies(predicate)` or [write your own extension](#extensible). The predicate runs inside the guard while the original argument remains available to report a failure.

### You don't have to remember this
The analyzers are bundled in the NuGet package and run during compilation:

| Diagnostic | Incorrect usage | Correct usage |
| --- | --- | --- |
| `Guards02` | `guard.Equals(value)` | `guard.EqualTo(value)` |
| `Guards03` | `multipleGuard.Equals(value)` | Validate the individual arguments explicitly |
| `Guards04` | `Guard.Argument(double.IsFinite(accuracy), nameof(accuracy)).EqualTo(true)` | `Guard.Argument(accuracy).Finite()` |
| `Guards05` | `Guard.Argument(isEnabled).EqualTo(true)` | `Guard.Argument(isEnabled).True()` |
| `Guards06` | Guard a collection count, distinct count or string length | Use `Empty`, `NotEmpty`, `Unique`, `UniqueBy`, `MinLength` or `MaxLength` on the original value |

Computed-condition detection covers `Guard.Argument(...)`, `Guard.Arguments(...)`, the `using static GuardProvider` shortcut and the `.Guard()` object extension, including explicit argument names. A plain `bool` argument or property, such as `Guard.Argument(options.IsEnabled).True()`, is valid. Replacing the terminal method alone does not repair `Guard.Argument(double.IsFinite(accuracy)).True()`.

For nullable booleans, `True()` and `False()` both reject null. `NotEqualTo(true)` allows null, so the analyzer does not suggest replacing it with `False()`.

Projects can enforce the diagnostics as errors in `.editorconfig`:

```ini
[*.cs]
dotnet_diagnostic.Guards04.severity = error
dotnet_diagnostic.Guards05.severity = error
dotnet_diagnostic.Guards06.severity = error
```

### `EnumDefined` and `[Flags]`
`EnumDefined` is built on `Enum.IsDefined`, which only recognises values *declared* in the enum. A legitimate combination of flags is not declared, so it is reported as undefined:

```C#
[Flags]
enum Permissions { Read = 1, Write = 2 }

Guard.Argument(Permissions.Read | Permissions.Write).EnumDefined();   // throws
```

Don't use `EnumDefined` on `[Flags]` enums.

## Documentation
### Documentation written in C# may be harder to read, but it never lies and it's always up to date.
https://github.com/martindybal/Dybal.Utils/tree/main/src/Utils/Dybal.Utils.Guards

### Many examples of use can be found in the tests
https://github.com/martindybal/Dybal.Utils/tree/main/src/Tests/Tests.Dybal.Utils.Guards

## Extensible
As you can see, I primarily use extension methods. So if you're missing a Guard, it's easy to write one. If you think it's generic enough, send a PR. I'm in!

```C#
public static class CustomGuardExtensions
{
    public static ArgumentGuard<int> IsNotFive(this ArgumentGuard<int> guard)
    {
        if (guard.Argument.Value == 5)
        {
            ThrowHelper.Throw<ArgumentException>(guard, "Value cannot be five.");
        }

        return guard;
    }
}
```
