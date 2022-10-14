# Dybal.Utils.Guards
The primary purpose of Dybal.Utils.Guards is to simplify writing input data checks to the object.

## Expected breaking change
First thing first. I wanted to implement this at the beginning, but was hampered by the lack of support for the nullable implicit operator in roslyn. Currently methods return TArgument directly. When https://github.com/dotnet/roslyn/issues/64638 is resolved, I want to change the return value to Guard with an implicit operator on TArgument. This allows the use of multiple guards at the same time. Prevention from this breaking change is not to use `var` but the explicit type.

```C#
Bar = Guard.Argument(bar).NotNullOrWhiteSpace().MaxLength(20); //Remains unchanged implicit conversion is used
string b1 = Guard.Argument(bar).NotNullOrWhiteSpace().MaxLength(20); //Remains unchanged implicit conversion is used

// Danger
var b2 = Guard.Argument(bar).NotNullOrWhiteSpace().MaxLength(20); //This is where the change occurs, the type will change from TArgument to Guard<TArgument>.
Bar = b2; // Once the change is implemented, it will need to be adjusted to `Bar = b2.Argument.Value;`
```

# Make your code readable as much as you can
```C#
Foo(string bar)
{
    if(string.NotNullOrWhiteSpace())
    {
        throw new ArgumentException("Value cannot be null or white space string.", nameof(Bar));
    }

    Bar = bar;
}
```

```C#
Foo(string bar)
{
    Bar = Guard.Argument(bar).NotNullOrWhiteSpace();
}
```
## Documentation
### C# documentation may be harder to read, but it never lies and it's up to date.
https://github.com/martindybal/Dybal.Utils/tree/main/src/Utils/Dybal.Utils.Guards

### Many examples of use can be found in the tests
https://github.com/martindybal/Dybal.Utils/tree/main/src/Tests/Tests.Dybal.Utils.Guards

## Extensible
As you can see, I primarily use extension methods. So if you're missing a Guard, it's easy to write one. If you think it's generic enough, send a PR. I'm on it!

```C#
public static class CustomGuardExtensions
{
    public static int IsNotZero(this Guard<int> guard)
    {
        if (guard.IsActive && guard.Argument.Value == 0)
        {
            throw new ArgumentException("Value cannot be zero.", guard.Argument.Name);
        }

        return guard.Argument.Value;
    }
}
```