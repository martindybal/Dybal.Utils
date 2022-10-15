namespace Dybal.Utils.Guards;

public interface IArgumentGuard<out TArgument>
{
    bool IsActive { get; }
    TArgument ArgumentValue { get; }
    string ArgumentName { get; }
    IArgumentGuard<TArgument?> If(bool condition);
}