using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> GreaterThanOrEqualTo<TArgument>(this ArgumentGuard<TArgument> guard, TArgument value, string? message = null, [CallerArgumentExpression("value")] string valueName = "")
        where TArgument : IComparable<TArgument>
    {
        if (guard.Argument.Value.CompareTo(value) < 0)
        {
            var defaultMessage = $"Value of parameter '{guard.Argument.Name}' ({guard.Argument.Value}) must be greater or equal than value of parameter '{valueName}' ({value}).";
            ThrowHelper.Throw<ArgumentException>(guard, message ?? defaultMessage);
        }

        return guard;
    }
}