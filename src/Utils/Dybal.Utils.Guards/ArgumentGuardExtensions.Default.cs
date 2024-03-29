﻿namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TArgument> Default<TArgument>(this ArgumentGuard<TArgument> guard, string? message = null)
        where TArgument : struct
    {
        if (!guard.Argument.Value.Equals(default(TArgument)))
        {
            var defaultMessage = guard.Argument.Value is Guid ?
                                    "Value must be an empty GUID." :
                                    "Value must be a default value.";
            ThrowHelper.Throw<ArgumentException>(guard, message ?? defaultMessage);
        }

        return guard;
    }
}