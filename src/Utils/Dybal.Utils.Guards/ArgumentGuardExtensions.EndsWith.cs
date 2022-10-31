namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> EndsWith(this ArgumentGuard<string> guard, string value, string? message = null)
    {
        return EndsWith(guard, value, StringComparison.CurrentCulture, message);
    }

    public static ArgumentGuard<string> EndsWith(this ArgumentGuard<string> guard, string value, StringComparison comparisonType, string? message = null)
    {
        if (!guard.Argument.Value.EndsWith(value, comparisonType))
        {
            message ??= $"\"{guard.Argument.Value}\" has to ends with \"{value}\".";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}