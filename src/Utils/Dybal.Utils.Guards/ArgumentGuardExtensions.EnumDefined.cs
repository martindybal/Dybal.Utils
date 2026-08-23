namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static ArgumentGuard<TEnum> EnumDefined<TEnum>(this ArgumentGuard<TEnum> guard, string? message = null)
        where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(guard.Argument.Value))
        {
            message ??= NotDefinedMessage<TEnum>(guard.Argument.Name, guard.Argument.Value);
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return guard;
    }

    public static ArgumentGuard<TEnum> EnumDefined<TEnum>(this ArgumentGuard<TEnum?> guard, string? message = null)
        where TEnum : struct, Enum
    {
        var value = guard.Argument.Value;

        if (!value.HasValue || !Enum.IsDefined(value.Value))
        {
            message ??= NotDefinedMessage<TEnum>(guard.Argument.Name, value);
            ThrowHelper.Throw<ArgumentException>(guard, message);
        }

        return ArgumentGuard<TEnum>.From(guard, new Argument<TEnum>(value.Value, guard.Argument.Name));
    }

    private static string NotDefinedMessage<TEnum>(string name, TEnum? value)
        where TEnum : struct, Enum
    {
        var valueRepresentation = value.HasValue ? value.Value.ToString() : "null";
        return $"Value of parameter '{name}' ({valueRepresentation}) must be defined in enum '{typeof(TEnum).Name}'.";
    }
}
