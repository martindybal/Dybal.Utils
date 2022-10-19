using System.Diagnostics.CodeAnalysis;

namespace Dybal.Utils.Guards;

internal readonly record struct ArgumentGuard<TArgument> : IArgumentGuard<TArgument>
{
    public IArgument<TArgument> Argument { get; }
    private Type? ExceptionOverrideType { get; init; }

    internal ArgumentGuard(IArgument<TArgument> argument)
    {
        Argument = argument;
        ExceptionOverrideType = null;
    }

    public IArgumentGuard<TArgument> With<TException>()
        where TException : Exception
    {
        return this with { ExceptionOverrideType = typeof(TException) };
    }

    [DoesNotReturn]
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
}