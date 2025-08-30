using System.Text.RegularExpressions;

namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<string> MatchesPattern(this ArgumentGuard<string> guard, string pattern, RegexOptions options = RegexOptions.None, string? message = null)
    {
        if (!Regex.IsMatch(guard.Argument.Value, pattern, options))
        {
            message ??= $"\"{guard.Argument.Value}\" has to match pattern \"{pattern}\".";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}
