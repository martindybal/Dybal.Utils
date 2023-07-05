# Dybal.Utils.Guards.ObjectExtensions
Extension method to facilitate the creation of a [ArgumentGuard](https://github.com/martindybal/Dybal.Utils/tree/main/src/Utils/Dybal.Utils.Guards) from a variable.

You can use the shortcut `id.Guard()` instead of `Guard.Argument(id)`. It's up to you what you prefer!

```C#
Foo(Guid id, string bar)
{
    Id = Guard.Argument(id).NotDefault();
    Bar = Guard.Argument(bar).NotNullOrWhiteSpace().MinLength(5).MaxLength(20);
}
```

```C#
Foo(Guid? id, string bar)
{
    Id = id.Guard().NotDefault();
    Bar = bar.Guard().NotNullOrWhiteSpace().MinLength(5).MaxLength(20);
}
```