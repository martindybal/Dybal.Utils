namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<double> Finite(this ArgumentGuard<double> guard, string? message = null)
    {
        if (!double.IsFinite(guard.Argument.Value))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"Value of parameter '{guard.Argument.Name}' ({guard.Argument.Value}) must be a finite number.");
        }

        return guard;
    }

    /// <summary>Requires a finite number when a value is present; null is allowed.</summary>
    public static ArgumentGuard<double?> Finite(this ArgumentGuard<double?> guard, string? message = null)
    {
        if (guard.Argument.Value is { } value && !double.IsFinite(value))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"Value of parameter '{guard.Argument.Name}' ({value}) must be a finite number.");
        }

        return guard;
    }

    public static ArgumentGuard<float> Finite(this ArgumentGuard<float> guard, string? message = null)
    {
        if (!float.IsFinite(guard.Argument.Value))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"Value of parameter '{guard.Argument.Name}' ({guard.Argument.Value}) must be a finite number.");
        }

        return guard;
    }

    /// <summary>Requires a finite number when a value is present; null is allowed.</summary>
    public static ArgumentGuard<float?> Finite(this ArgumentGuard<float?> guard, string? message = null)
    {
        if (guard.Argument.Value is { } value && !float.IsFinite(value))
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"Value of parameter '{guard.Argument.Name}' ({value}) must be a finite number.");
        }

        return guard;
    }
}
