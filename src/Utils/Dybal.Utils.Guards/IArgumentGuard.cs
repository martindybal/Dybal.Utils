namespace Dybal.Utils.Guards;

public interface IArgumentGuard<out TArgument>
{
    bool IsActive { get; }
    IArgument<TArgument> Argument { get; }

    IConditionalArgumentGuard<TArgument?> If(bool condition);

    IArgumentGuard<TArgument> With<TException>()
        where TException : Exception;

    void Throw<TException>(string? message)
        where TException : Exception;
}