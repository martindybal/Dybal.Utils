using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class GuardExtensions
{
    public static TArgument NotNull<TArgument>(this Guard<TArgument> guard, string? message = null)
    {
        if (guard.IsActive && guard.Argument.Value is null)
        {
            throw message == null
                ? new ArgumentNullException(guard.Argument.Name)
                : new ArgumentNullException(guard.Argument.Name, message);
        }

        return guard.Argument.Value;
    }
}