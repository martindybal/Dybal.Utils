namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static IEnumerator<TArgument> GetEnumerator<TArgument>(this ICovariantArgumentGuard<IEnumerable<TArgument>?> covariantGuard)
    {
        return covariantGuard.NotNull().Argument.Value.GetEnumerator();
    }
}
