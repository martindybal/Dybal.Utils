using System;

namespace Dybal.Utils.Guards;

public record struct MultipleArgumentGuard : IExceptionOverride
{
    public IReadOnlyList<IArgument<object?>> Arguments { get; }

    string IExceptionOverride.ArgumentName => string.Join(", ", Arguments.Select(static argument => argument.Name));

    Type? IExceptionOverride.ExceptionOverrideType => ExceptionOverrideType;
    internal Type? ExceptionOverrideType { get; private init; }

    internal MultipleArgumentGuard(IReadOnlyList<IArgument<object?>> arguments)
    {
        Arguments = arguments;
        ExceptionOverrideType = null;
    }
    
    public MultipleArgumentGuard Throws<TException>()
        where TException : Exception
    {
#if DEBUG
        ThrowHelper.EnsureRegistration<TException>();
#endif
        return this with { ExceptionOverrideType = typeof(TException) };
    }
}