namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> ContainsOnly(this ICovariantArgumentGuard<string?> covariantGuard, char[] allowedCharacters, string? message = null)
    {
        var guard = ArgumentGuard<string>.From(covariantGuard, covariantGuard.Argument!).NotNull(message);
        ArgumentGuard<char[]>.From(guard, new Argument<char[]>(allowedCharacters, nameof(allowedCharacters))).NotNull();

        var alphabet = new HashSet<char>(allowedCharacters);
        foreach (var character in guard.Argument.Value)
        {
            if (!alphabet.Contains(character))
            {
                ThrowHelper.Throw<ArgumentException>(guard, message ?? "Value must contain only characters from the allowed alphabet.");
            }
        }

        return guard;
    }
}
