using Dybal.Utils.TypedValues;

namespace Tests.Dybal.Utils.TypedValues;

[TypedValue<string>]
public readonly partial record struct TestTypedReadonlyString
{
    static partial void ValidateValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value can't be null or white space", nameof(value));
        }
    }
}