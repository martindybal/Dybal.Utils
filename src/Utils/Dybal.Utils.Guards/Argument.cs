namespace Dybal.Utils.Guards;

public readonly record struct Argument<TArgument>(TArgument Value, string Name) : IArgument<TArgument>
{
    public TArgument Value { get; } = Value;
    public string Name { get; } = Name;
}