﻿namespace Dybal.Utils.Guards;

public readonly record struct MultipleArgumentGuard : IExceptionOverride
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
        ThrowHelper.EnsureRegistrationInDebug<TException>();
        return this with { ExceptionOverrideType = typeof(TException) };
    }
}