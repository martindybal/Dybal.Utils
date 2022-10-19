using System.Diagnostics.CodeAnalysis;

namespace Dybal.Utils.Guards;

public interface IArgumentGuard<out TArgument> : IExceptionOverride
{
    IArgument<TArgument> Argument { get; }
    string IExceptionOverride.ArgumentName => Argument.Name;

    IArgumentGuard<TArgument> Throws<TException>()
        where TException : Exception;
}