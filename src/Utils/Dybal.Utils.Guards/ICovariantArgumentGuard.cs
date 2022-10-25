namespace Dybal.Utils.Guards;

public interface ICovariantArgumentGuard<out TArgument>: IExceptionOverride
{
    IArgument<TArgument> Argument { get; }
}