namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TEnumerable> DoesNotContain<TEnumerable, TArgument>(this ICovariantArgumentGuard<TEnumerable> covariantGuard, TArgument? value, string? message = null)
        where TEnumerable : IEnumerable<TArgument>
    {
        var guard = ArgumentGuard<TEnumerable>.From(covariantGuard.ArgumentValue, covariantGuard.ArgumentName, covariantGuard.ExceptionOverrideType);
        message ??= $"Collection must not contain '{value}'.";
        return guard.None<TEnumerable, TArgument>(item => Equals(item, value), message);
    }
}