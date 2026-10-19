namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> DoesNotContainDuplicateCharacters(this ICovariantArgumentGuard<string?> covariantGuard, string? message = null)
    {
        var guard = ArgumentGuard<string>.From(covariantGuard, covariantGuard.Argument!).NotNull(message);
        var seen = new HashSet<char>();
        foreach (var character in guard.Argument.Value)
        {
            if (!seen.Add(character))
            {
                ThrowHelper.Throw<ArgumentException>(guard, message ?? "Value must not contain duplicate characters.");
            }
        }

        return guard;
    }
}
