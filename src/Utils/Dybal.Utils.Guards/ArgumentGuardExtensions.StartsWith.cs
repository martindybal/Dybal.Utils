namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> StartsWith(this ArgumentGuard<string> guard, string value, string? message = null)
    {
        return StartsWith(guard, value, StringComparison.CurrentCulture, message);
    }

    public static ArgumentGuard<string> StartsWith(this ArgumentGuard<string> guard, string value, StringComparison comparisonType, string? message = null)
    {
        if (!guard.Argument.Value.StartsWith(value, comparisonType))
        {
            message ??= $"\"{guard.Argument.Value}\" has to start with \"{value}\".";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}