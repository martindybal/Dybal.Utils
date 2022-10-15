using System.Runtime.CompilerServices;
using Dybal.Utils.Guards;
using Xunit;

namespace Tests.Dybal.Utils.Guards;

public class AssertGuard
{
    public static void AssertArgument<T>(T value, Argument<object?> argument, [CallerArgumentExpression("value")] string? valueName = null)
    {
        Assert.Equal(valueName, argument.Name);
        Assert.Equal(value, argument.Value);
    }
}