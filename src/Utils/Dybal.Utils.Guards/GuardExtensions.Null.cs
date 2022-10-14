using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class GuardExtensions
{
    public static TArgument? Null<TArgument>(this Guard<TArgument> guard, string? message = null)
    {
        if (guard.IsActive && guard.Argument.Value is not null)
        {
            throw new ArgumentException(message ?? "Value must be null.", guard.Argument.Name);
        }

        return guard.Argument.Value;
    }
}