using System.Runtime.CompilerServices;
using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class AssertGuard
{
    public static void AssertArgument<T>(T value, Argument<object?> argument, [CallerArgumentExpression("value")] string? valueName = null)
    {
        AssertArgument(value, argument.Value, argument.Name, valueName);
    }

    public static void AssertArgument<T>(T value, T argumentValue, string argumentName, [CallerArgumentExpression("value")] string? valueName = null)
    {
        Assert.Equal(valueName, argumentName);
        Assert.Equal(value, argumentValue);
    }
}