namespace Dybal.Utils.Guards;

public record struct ArgumentGuard<TArgument> : IArgumentGuard<TArgument>
{
    public bool IsActive { get; private set; }
    public TArgument ArgumentValue { get; }
    public string ArgumentName { get; }
    
    internal ArgumentGuard(TArgument argumentValue, string argumentName, bool isActive)
    {
        ArgumentValue = argumentValue;
        ArgumentName = argumentName;
        IsActive = isActive;
        IsActive = isActive;
    }

    public ArgumentGuard<TArgument?> If(bool condition)
    {
        if (!condition)
        {
            IsActive = false;
        }
        return this!;
    }
}