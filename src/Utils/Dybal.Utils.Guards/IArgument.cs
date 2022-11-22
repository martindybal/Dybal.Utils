namespace Dybal.Utils.Guards;

public interface IArgument<out TArgument>
{
    TArgument Value { get; }
    string Name { get; }
}