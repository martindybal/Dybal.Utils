using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class GuardExtensions
{
    public static Guard<TArgument?> IfHasValue<TArgument>(this Guard<TArgument?> guard)
        where TArgument : struct
    {
        return guard.If(guard.Argument.Value.HasValue);
    }
}