namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<int> DivisibleBy(this ArgumentGuard<int> guard, int divisor, string? message = null)
    {
        if (divisor == 0)
        {
            var divisorGuard = ArgumentGuard<int>.From(guard, new Argument<int>(divisor, nameof(divisor)));
            ThrowHelper.Throw<ArgumentOutOfRangeException>(divisorGuard, "Divisor must not be zero.");
        }

        if ((long)guard.Argument.Value % divisor != 0)
        {
            ThrowHelper.Throw<ArgumentException>(guard, message ?? $"Value must be divisible by {divisor}.");
        }

        return guard;
    }
}
