namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<DateTime> Utc(this ArgumentGuard<DateTime> guard, string? message = null)
    {
        if (guard.Argument.Value.Kind != DateTimeKind.Utc)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"Kind of parameter '{guard.Argument.Name}' must be {DateTimeKind.Utc} but was {guard.Argument.Value.Kind}.");
        }

        return guard;
    }
}
