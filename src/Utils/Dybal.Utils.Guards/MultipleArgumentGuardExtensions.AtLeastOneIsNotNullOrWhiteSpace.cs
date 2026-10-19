namespace Dybal.Utils.Guards;

public static partial class MultipleArgumentGuardExtensions
{
    public static MultipleArgumentGuard AtLeastOneIsNotNullOrWhiteSpace(this MultipleArgumentGuard guard, string? message = null)
    {
        var hasNonBlankValue = false;
        foreach (var argument in guard.Arguments)
        {
            if (argument.Value is null)
            {
                continue;
            }

            if (argument.Value is string value)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    hasNonBlankValue = true;
                }
            }
            else
            {
                ThrowHelper.Throw<ArgumentException>(guard, message ?? "All arguments must be strings or null.");
            }
        }

        if (!hasNonBlankValue)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? "At least one argument must be a non-whitespace string.");
        }

        return guard;
    }
}
