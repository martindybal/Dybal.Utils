namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> EndsWith(this ArgumentGuard<string> guard, string value, string? message = null)
    {
        if (!guard.Argument.Value.EndsWith(value))
        {
            message ??= $"\"{guard.Argument.Value}\" has to ends with \"{value}\".";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}