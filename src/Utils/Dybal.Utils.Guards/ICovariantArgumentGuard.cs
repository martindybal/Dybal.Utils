namespace Dybal.Utils.Guards;

public interface ICovariantArgumentGuard<out TArgument>: IExceptionOverride
{
    TArgument ArgumentValue { get; }
}