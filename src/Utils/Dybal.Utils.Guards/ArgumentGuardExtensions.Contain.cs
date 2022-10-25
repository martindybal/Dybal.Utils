namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TEnumerable> Contain<TEnumerable, TArgument>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, TArgument? value, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard, covariantGuard.Argument);
        message ??= $"Collection has to contain '{value}'.";
        return guard.Any<TEnumerable, TArgument>(item => Equals(item, value), message);
    }
}