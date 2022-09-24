using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class Guard
{
    public static void AtLeastOneIsNotNull(object? firstValue,
                                           object? secondValue,
                                           [CallerArgumentExpression("firstValue")] string? firstParamName = null,
                                           [CallerArgumentExpression("secondValue")] string? secondParamName = null)

    {
        var values = new[] { firstValue, secondValue };
        var paramNames = new[] { firstParamName, secondParamName };
        AtLeastOneIsNotNull(values, paramNames);
    }

    public static void AtLeastOneIsNotNull(object? firstValue,
                                           object? secondValue,
                                           object? thirdValue,
                                           [CallerArgumentExpression("firstValue")] string? firstParamName = null,
                                           [CallerArgumentExpression("secondValue")] string? secondParamName = null,
                                           [CallerArgumentExpression("thirdValue")] string? thirdParamName = null)

    {
        var values = new[] { firstValue, secondValue, thirdValue };
        var paramNames = new[] { firstParamName, secondParamName, thirdParamName };
        AtLeastOneIsNotNull(values, paramNames);
    }

    private static void AtLeastOneIsNotNull(object?[] values, string?[] paramNames)
    {
        if (values.All(value => value is null))
        {
            throw new ArgumentException($"Some parameters {string.Join(", ", paramNames)} must be not null.");
        }
    }
}