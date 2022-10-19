using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static class Guard
{
    /// <summary>
    /// ArgumentGuard&lt;TArgument&gt; factory method. 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="argumentValue">The argumentValue to be checked.</param>
    /// <param name="argumentName">DO NOT SET - Name of <c>argumentValue</c> parameter is self-replenishing.</param>
    /// <returns>Instance of ArgumentGuard&lt;TArgument&gt;</returns>
    public static IArgumentGuard<TArgument> Argument<TArgument>(TArgument argumentValue, [CallerArgumentExpression("argumentValue")] string? argumentName = null)
    {
        return new ArgumentGuard<TArgument>(new Argument<TArgument>(argumentValue, argumentName!));
    }

    public static MultipleArgumentGuard Arguments<TArgument1, TArgument2>(
        TArgument1 firstValue,
        TArgument2 secondValue,
        [CallerArgumentExpression("firstValue")] string? firstParamName = null,
        [CallerArgumentExpression("secondValue")] string? secondParamName = null)
    {
        var firstArgument = new Argument<object?>(firstValue, firstParamName!);
        var secondArgument = new Argument<object?>(secondValue, secondParamName!);

        return Arguments(new CompactList<IArgument<object?>>(firstArgument, secondArgument));
    }

    public static MultipleArgumentGuard Arguments<TArgument1, TArgument2, TArgument3>(
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

        return Arguments(new CompactList<IArgument<object?>>(firstArgument, secondArgument, thirdArgument));
    }

    internal static MultipleArgumentGuard Arguments(CompactList<IArgument<object?>> arguments)
    {
        return new MultipleArgumentGuard(arguments, true);
    }
}