namespace Dybal.Utils.Guards;

internal record struct ArgumentGuard<TArgument> : IArgumentGuard<TArgument>, IConditionalArgumentGuard<TArgument>
{
    public bool IsActive { get; private set; }
    public IArgument<TArgument> Argument { get; }
    private Type? ExceptionOverrideType { get; init; }

    internal ArgumentGuard(IArgument<TArgument> argument, bool isActive)
    {
        Argument = argument;
        ExceptionOverrideType = null;
        IsActive = isActive;
    }

    public IArgumentGuard<TArgument> With<TException>()
        where TException : Exception
    {
        return this with { ExceptionOverrideType = typeof(TException) };
    }

    public void Throw<TException>(string? message) 
        where TException : Exception
    {
        if (ExceptionOverrideType is null)
        {
            ThrowHelper.Throw<TException>(Argument.Name, message);
        }
        else
        {
            ThrowHelper.Throw(ExceptionOverrideType, Argument.Name, message);
        }
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