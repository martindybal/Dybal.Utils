using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> NotEqualTo<TArgument>(this ArgumentGuard<TArgument> guard, TArgument value, string? message = null, [CallerArgumentExpression("value")] string valueName = "")
        where TArgument : IEquatable<TArgument>
    {
        if (guard.Argument.Value.Equals(value))
        {
            var defaultMessage = $"Value of parameter '{guard.Argument.Name}' ({guard.Argument.Value}) must not be equal to value of parameter '{valueName}' ({value}).";
            ThrowHelper.Throw<ArgumentException>(guard, message ?? defaultMessage);
        }

        return guard;
    }
}