namespace Dybal.Utils.Guards;

public interface ICovariantArgumentGuard<out TArgument>
{
    IArgument<TArgument> Argument { get; }
}