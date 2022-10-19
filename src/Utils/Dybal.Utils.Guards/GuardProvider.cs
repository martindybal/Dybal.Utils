using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

/// <summary>
/// This class serves as a shortcut for ArgumentGuard.Argument. If you want to use it, use static using.
/// </summary>
/// <code>
/// using static Dybal.Utils.Guards.GuardProvider;
///
/// ArgumentGuard(value).NotNull();
/// </code>
public static class GuardProvider
{
    /// <summary>
    /// ArgumentGuard.Argument shortcut. 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="argumentValue">The argumentValue to be checked.</param>
    /// <param name="argumentName">DO NOT SET - Name of <c>argumentValue</c> parameter is self-replenishing.</param>
    /// <returns>Instance of ArgumentGuard&lt;TArgument&gt;</returns>
    public static IArgumentGuard<TArgument> Guard<TArgument>(TArgument argumentValue, [CallerArgumentExpression("argumentValue")] string? argumentName = null)
    {
        return Guards.Guard.Argument(argumentValue, argumentName);
    }
    
    public static MultipleArgumentGuard Guard<TArgument1, TArgument2>(
        TArgument1 firstValue,
        TArgument2 secondValue,
        [CallerArgumentExpression("firstValue")] string? firstParamName = null,
        [CallerArgumentExpression("secondValue")] string? secondParamName = null)
    {
        var firstArgument = new Argument<object?>(firstValue, firstParamName!);
        var secondArgument = new Argument<object?>(secondValue, secondParamName!);

        return Guards.Guard.Arguments(new CompactList<IArgument<object?>>(firstArgument, secondArgument));
    }

    public static MultipleArgumentGuard Guard<TArgument1, TArgument2, TArgument3>(
        TArgument1 firstValue,
        TArgument2 secondValue,
        TArgument3 thirdValue,
        [CallerArgumentExpression("firstValue")] string? firstParamName = null,
        [CallerArgumentExpression("secondValue")] string? secondParamName = null,
        [CallerArgumentExpression("thirdValue")] string? thirdParamName = null)
    {
        var firstArgument = new Argument<object?>(firstValue, firstParamName!);
        var secondArgument = new Argument<object?>(secondValue, secondParamName!);
        var thirdArgument = new Argument<object?>(thirdValue, thirdParamName!);

        return Guards.Guard.Arguments(new CompactList<IArgument<object?>>(firstArgument, secondArgument, thirdArgument));
    }

    public static MultipleArgumentGuard Guard(params IArgument<object?>[] arguments)
    {
        return new MultipleArgumentGuard(arguments);
    }
}