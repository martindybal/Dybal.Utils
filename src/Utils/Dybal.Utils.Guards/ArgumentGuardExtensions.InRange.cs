namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<T> InRange<T>(this ArgumentGuard<T> guard, T min, T max, bool inclusive = true)
        where T : IComparable<T>
    {
        var value = guard.Argument.Value;
        var isInRange = inclusive
            ? value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0
            : value.CompareTo(min) > 0 && value.CompareTo(max) < 0;

        if (!isInRange)
        {
            var rangeRepresentation = inclusive ? $"[{min}, {max}]" : $"({min}, {max})";
            var defaultMessage = $"Value of parameter '{guard.Argument.Name}' ({value}) must be in the range {rangeRepresentation}.";
            ThrowHelper.Throw<ArgumentOutOfRangeException>(guard, defaultMessage);
        }

        return guard;
    }
}

