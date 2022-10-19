namespace Dybal.Utils.Guards;

public interface IArgumentGuard<out TArgument>
{
    bool IsActive { get; }
    IArgument<TArgument> Argument { get; }

    IConditionalArgumentGuard<TArgument?> If(bool condition);
}