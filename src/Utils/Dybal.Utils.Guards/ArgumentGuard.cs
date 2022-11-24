namespace Dybal.Utils.Guards;

public readonly record struct ArgumentGuard<TArgument> : ICovariantArgumentGuard<TArgument>, IExceptionOverride
{
    public TArgument ArgumentValue { get; }
    public string ArgumentName { get; }
    
    internal Type? ExceptionOverrideType { get; private init; }

    string IExceptionOverride.ArgumentName => ArgumentName;
    Type? IExceptionOverride.ExceptionOverrideType => ExceptionOverrideType;

    internal ArgumentGuard(TArgument argumentValue, string argumentName)
        : this(argumentValue, argumentName, null)
    {
    }

    private ArgumentGuard(TArgument argumentValue, string argumentName, Type? exceptionOverrideType)
    {
        ArgumentValue = argumentValue;
        ArgumentName = argumentName;
        ExceptionOverrideType = exceptionOverrideType;
    }
    
    public static implicit operator TArgument(ArgumentGuard<TArgument> guard) => guard.ArgumentValue;

    public ArgumentGuard<TArgument> Throws<TException>()
        where TException : Exception
    {
        ThrowHelper.EnsureRegistrationInDebug<TException>();
        return this with { ExceptionOverrideType = typeof(TException) };
    }
    
    public static ArgumentGuard<TArgument> From(IExceptionOverride exceptionOverride, TArgument argumentValue, string argumentName)
    {
        //TODO use just ExceptionOverrideType
        return new ArgumentGuard<TArgument>(argumentValue, argumentName, exceptionOverride.ExceptionOverrideType);
    }
}