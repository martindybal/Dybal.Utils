using System.Runtime.CompilerServices;
using Dybal.Utils.Guards;

namespace Dybal.Utils.Guards.ObjectExtensions;

public static class GuardObjectExtensionsExtensions
{
    public static Guard<TArgument> Guard<TArgument>(this TArgument argumentValue, [CallerArgumentExpression("argumentValue")] string? argumentName = null)
    {
        return Guards.Guard.Argument(argumentValue, argumentName);
    }
}