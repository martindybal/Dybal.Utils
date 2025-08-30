namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TEnum> EnumDefined<TEnum>(this ArgumentGuard<TEnum> guard, string? message = null)
        where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(guard.Argument.Value))
        {
            message ??= $"Value of parameter '{guard.Argument.Name}' ({guard.Argument.Value}) must be defined in enum '{typeof(TEnum).Name}'.";
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }
}
