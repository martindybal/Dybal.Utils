namespace Dybal.Utils.Guards;

internal record struct ArgumentGuard<TArgument> : IArgumentGuard<TArgument>, IConditionalArgumentGuard<TArgument>
{
    public bool IsActive { get; private set; }
    public IArgument<TArgument> Argument { get; }

    internal ArgumentGuard(IArgument<TArgument> argument, bool isActive)
    {
        Argument = argument;
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