using System.Diagnostics.CodeAnalysis;

namespace Dybal.Utils.Guards;

internal readonly record struct ArgumentGuard<TArgument> : IArgumentGuard<TArgument>
{
    public IArgument<TArgument> Argument { get; }
    
    Type? IExceptionOverride.ExceptionOverrideType => ExceptionOverrideType;
    internal Type? ExceptionOverrideType { get; private init; }

    internal ArgumentGuard(IArgument<TArgument> argument)
    {
        Argument = argument;
        ExceptionOverrideType = null;
    }

    public IArgumentGuard<TArgument> Throws<TException>()
        where TException : Exception
    {
#if DEBUG
        ThrowHelper.EnsureRegistration<TException>();
#endif
        return this with { ExceptionOverrideType = typeof(TException) };
    }
}