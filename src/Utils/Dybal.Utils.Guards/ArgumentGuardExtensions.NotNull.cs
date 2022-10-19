﻿namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static TArgument? NotNull<TArgument>(this IConditionalArgumentGuard<TArgument?> guard, string? message = null)
    {
        return ((IArgumentGuard<TArgument>)guard).NotNull(message);
    }

    public static TArgument NotNull<TArgument>(this IArgumentGuard<TArgument?> guard, string? message = null)
    {
        if (guard.IsActive)
        {
            if (guard.Argument.Value is null)
            {
                ThrowHelper.Throw<ArgumentNullException>(guard.Argument.Name, message);
            }
        }

        return guard.Argument.Value!;
    }
}