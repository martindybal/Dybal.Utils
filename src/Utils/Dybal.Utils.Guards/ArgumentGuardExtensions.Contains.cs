namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TEnumerable> Contains<TEnumerable, TArgument>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, TArgument? value, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard, covariantGuard.Argument);
        message ??= $"Collection has to contain '{value}'.";
        return guard.Any<TEnumerable, TArgument>(item => Equals(item, value), message);
    }

    public static ArgumentGuard<string> Contains(this ArgumentGuard<string> guard, string value, StringComparison comparisonType = StringComparison.CurrentCulture, string? message = null)
    {
        if (!guard.Argument.Value.Contains(value, comparisonType))
        {
            message ??= $"\"{guard.Argument.Value}\" has to contain \"{value}\".";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}