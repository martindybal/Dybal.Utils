namespace Dybal.Utils.Guards;

internal record struct ArgumentGuard<TArgument> : IArgumentGuard<TArgument>, IConditionalArgumentGuard<TArgument>
{
    public bool IsActive { get; private set; }
    public TArgument ArgumentValue { get; }
    public string ArgumentName { get; }

    internal ArgumentGuard(TArgument argumentValue, string argumentName, bool isActive)
    {
        ArgumentValue = argumentValue;
        ArgumentName = argumentName;
        IsActive = isActive;
    }
    
    public IConditionalArgumentGuard<TArgument?> If(bool condition)
    {
        if (!condition)
        {
            IsActive = false;
        }
        return this;
    }
}