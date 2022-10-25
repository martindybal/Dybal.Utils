using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards.ObjectExtensions;

public static class GuardObjectExtensionsExtensions
{
    public static ArgumentGuard<TArgument> Guard<TArgument>(this TArgument argumentValue, [CallerArgumentExpression("argumentValue")] string? argumentName = null)
    {
        return Guards.Guard.Argument(argumentValue, argumentName);
    }
}