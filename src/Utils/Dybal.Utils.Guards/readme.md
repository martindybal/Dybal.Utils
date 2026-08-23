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

1. **`ParamName` is garbage.** `Guard.Argument` fills the argument name from [`[CallerArgumentExpression]`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/caller-information#callerargumentexpression-attribute), which captures the *source text of whatever you wrote inside the parentheses* - not the variable it happens to read. That text is `Enum.IsDefined(state)`, so `ArgumentException.ParamName` is literally `"Enum.IsDefined(state)"`. Anything that keys off the parameter name breaks, for example `catch (ArgumentException ex) when (ex.ParamName == nameof(state))`.
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
Guard.Argument(bar).MinLength(5);            // ✅ ParamName is "bar"
```

If no guard fits your rule, [write your own extension](#extensible) rather than pre-computing a `bool` - that keeps the argument, and therefore its name and value, all the way to the exception.

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