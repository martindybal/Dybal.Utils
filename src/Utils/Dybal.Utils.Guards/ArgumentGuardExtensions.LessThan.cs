using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> LessThan<TArgument>(this ArgumentGuard<TArgument> guard, TArgument value, string? message = null, [CallerArgumentExpression("value")] string valueName = "")
        where TArgument : IComparable<TArgument>
    {
        if (guard.ArgumentValue.CompareTo(value) >= 0)
        {
            var defaultMessage = $"Value of parameter '{guard.ArgumentName}' ({guard.ArgumentValue}) must be less than value of parameter '{valueName}' ({value}).";
            ThrowHelper.Throw<ArgumentException>(guard, message ?? defaultMessage);
        }

        return guard;
    }
}