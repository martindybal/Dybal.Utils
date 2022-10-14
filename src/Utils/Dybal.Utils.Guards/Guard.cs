using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public partial class Guard<TArgument>
{
    public Argument<TArgument> Argument { get; }
    public bool IsActive { get; }

    internal Guard(Argument<TArgument> argument, bool isActive)
    {
        Argument = argument;
        IsActive = isActive;
    }

    public Guard<TArgument?> If(bool condition)
    {
        if (condition)
        {
            return this!;
        }

        return new Guard<TArgument?>(Argument!, false);
    }
}

public static class Guard
{
    /// <summary>
    /// Guard&lt;TArgument&gt; factory method. 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="argumentValue">The argumentValue to be checked.</param>
    /// <param name="argumentName">DO NOT SET - Name of <c>argumentValue</c> parameter is self-replenishing.</param>
    /// <returns>Instance of Guard&lt;TArgument&gt;</returns>
    public static Guard<TArgument> Argument<TArgument>(TArgument argumentValue, [CallerArgumentExpression("argumentValue")] string? argumentName = null)
    {
        return new Guard<TArgument>(new Argument<TArgument>(argumentValue, argumentName!), true);
    }
}

/// <summary>
/// This class serves as a shortcut for Guard.Argument. If you want to use it, use static using.
/// </summary>
/// <code>
/// using static Dybal.Utils.Guards.GuardProvider;
///
/// Guard(value).NotNull();
/// </code>
public static class GuardProvider
{
    /// <summary>
    /// Guard.Argument shortcut. 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="argumentValue">The argumentValue to be checked.</param>
    /// <param name="argumentName">DO NOT SET - Name of <c>argumentValue</c> parameter is self-replenishing.</param>
    /// <returns>Instance of Guard&lt;TArgument&gt;</returns>
    public static Guard<TArgument> Guard<TArgument>(TArgument argumentValue, [CallerArgumentExpression("argumentValue")] string? argumentName = null)
    {
        return Guards.Guard.Argument(argumentValue, argumentName);
    }
}
