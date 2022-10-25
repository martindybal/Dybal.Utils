namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> StartsWith(this ArgumentGuard<string> guard, string value, string? message = null)
    {
        if (!guard.Argument.Value.StartsWith(value))
        {
            message ??= $"\"{guard.Argument.Value}\" has to starts with \"{value}\".";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}