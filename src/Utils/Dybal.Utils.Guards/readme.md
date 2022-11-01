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

### Less boilerplate more clarity
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