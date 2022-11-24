namespace Dybal.Utils.Guards;

public readonly record struct ArgumentGuard<TArgument> : ICovariantArgumentGuard<TArgument>, IExceptionOverride
{
    public IArgument<TArgument> Argument { get; }
    internal Type? ExceptionOverrideType { get; private init; }

    string IExceptionOverride.ArgumentName => Argument.Name;
    Type? IExceptionOverride.ExceptionOverrideType => ExceptionOverrideType;

    internal ArgumentGuard(IArgument<TArgument> argument)
        : this(argument, null)
    {
    }

    private ArgumentGuard(IArgument<TArgument> argument, Type? exceptionOverrideType)
    {
        Argument = argument;
        ExceptionOverrideType = exceptionOverrideType;
    }

    public static implicit operator TArgument(ArgumentGuard<TArgument> guard) => guard.Argument.Value;

    public ArgumentGuard<TArgument> Throws<TException>()
        where TException : Exception
    {
        ThrowHelper.EnsureRegistrationInDebug<TException>();
        return this with { ExceptionOverrideType = typeof(TException) };
    }
    
    public static ArgumentGuard<TArgument> From(IExceptionOverride exceptionOverride, IArgument<TArgument> argument)
    {
        return new ArgumentGuard<TArgument>(argument, exceptionOverride.ExceptionOverrideType);
    }
}