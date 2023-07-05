# Dybal.Utils.TypedValues
The primary purpose of Dybal.Utils.TypedValues is to avoid mismatch simple values. Such as int, guid, string. And easy to create types with validations.

```
using Dybal.Utils.TypedValues;

[TypedValue<string>]
public readonly partial record struct Firstname;

[TypedValue<string>]
public readonly partial record struct Lastname;

public record Person(Firstname Firstname, Lastname Lastname);

public class Usage
{
    public Usage()
    {
        var person = new Person(new Firstname("Martin"), new Lastname("Dybal"));
    }
}
```

## Validation of value
```
[TypedValue<double>]
public readonly partial record struct Percentage
{
    static partial void ValidateValue(double value)
    {
        Guard.Argument(value).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
    }
}
```