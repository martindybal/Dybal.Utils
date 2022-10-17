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
            if (guard.ArgumentValue is null)
            {
                if (message == null)
                    ThrowHelper.ThrowArgumentNullException(guard.ArgumentName);

                ThrowHelper.ThrowArgumentNullException(guard.ArgumentName, message);
            }
        }

        return guard.ArgumentValue!;
    }
}