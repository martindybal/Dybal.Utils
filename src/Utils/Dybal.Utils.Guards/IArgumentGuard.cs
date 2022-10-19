using System.Diagnostics.CodeAnalysis;

namespace Dybal.Utils.Guards;

public interface IArgumentGuard<out TArgument>
{
    IArgument<TArgument> Argument { get; }
    
    IArgumentGuard<TArgument> With<TException>()
        where TException : Exception;

    [DoesNotReturn]
    void Throw<TException>(string? message)
        where TException : Exception;
}