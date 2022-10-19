namespace Dybal.Utils.Guards;

public record struct MultipleArgumentGuard
{
    public IReadOnlyList<IArgument<object?>> Arguments { get; }
    public bool IsActive { get; private set; }
    private Type? ExceptionOverrideType { get; init; }

    internal MultipleArgumentGuard(IReadOnlyList<IArgument<object?>> arguments, bool isActive)
    {
        Arguments = arguments;
        IsActive = isActive;
        ExceptionOverrideType = null;
    }
    
    public MultipleArgumentGuard If(bool condition)
    {
        if (!condition)
        {
            IsActive = false;
        }
        return this;
    }


    public MultipleArgumentGuard With<TException>()
        where TException : Exception
    {
        return this with { ExceptionOverrideType = typeof(TException) };
    }

    public void Throw<TException>(string? message) where TException : Exception
    {
        var argumentNames = string.Join(", ", Arguments.Select(static argument => argument.Name));
        if (ExceptionOverrideType is null)
        {
            ThrowHelper.Throw<TException>(argumentNames, message);
        }
        else
        {
            ThrowHelper.Throw(ExceptionOverrideType, argumentNames, message);
        }
    }
}