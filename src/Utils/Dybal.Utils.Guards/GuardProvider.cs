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
    public static ArgumentGuard<TArgument> Guard<TArgument>(TArgument argumentValue, [CallerArgumentExpression("argumentValue")] string? argumentName = null)
    {
        return Guards.Guard.Argument(argumentValue, argumentName);
    }

    public static MultipleArgumentGuard Guard<TArgument1, TArgument2>(
        TArgument1 value1,
        TArgument2 value2,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "")
    {
        return Guards.Guard.Arguments(value1, value2, paramName1, paramName2);
    }

    public static MultipleArgumentGuard Guard<TArgument1, TArgument2, TArgument3>(
        TArgument1 value1,
        TArgument2 value2,
        TArgument3 value3,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "",
        [CallerArgumentExpression("value3")] string paramName3 = "")
    {
        return Guards.Guard.Arguments(value1, value2, value3, paramName1, paramName2, paramName3);
    }

    public static MultipleArgumentGuard Guard<TArgument1, TArgument2, TArgument3, TArgument4>(
        TArgument1 value1,
        TArgument2 value2,
        TArgument3 value3,
        TArgument4 value4,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "",
        [CallerArgumentExpression("value3")] string paramName3 = "",
        [CallerArgumentExpression("value4")] string paramName4 = "")
    {
        return Guards.Guard.Arguments(value1, value2, value3, value4, paramName1, paramName2, paramName3, paramName4);
    }

    public static MultipleArgumentGuard Guard<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>(
        TArgument1 value1,
        TArgument2 value2,
        TArgument3 value3,
        TArgument4 value4,
        TArgument5 value5,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "",
        [CallerArgumentExpression("value3")] string paramName3 = "",
        [CallerArgumentExpression("value4")] string paramName4 = "",
        [CallerArgumentExpression("value5")] string paramName5 = "")
    {
        return Guards.Guard.Arguments(value1, value2, value3, value4, value5, paramName1, paramName2, paramName3, paramName4, paramName5);
    }

    public static MultipleArgumentGuard Guard<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6>(
        TArgument1 value1,
        TArgument2 value2,
        TArgument3 value3,
        TArgument4 value4,
        TArgument5 value5,
        TArgument6 value6,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "",
        [CallerArgumentExpression("value3")] string paramName3 = "",
        [CallerArgumentExpression("value4")] string paramName4 = "",
        [CallerArgumentExpression("value5")] string paramName5 = "",
        [CallerArgumentExpression("value6")] string paramName6 = "")
    {
        return Guards.Guard.Arguments(value1, value2, value3, value4, value5, value6, paramName1, paramName2, paramName3, paramName4, paramName5, paramName6);
    }

    public static MultipleArgumentGuard Guard<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7>(
        TArgument1 value1,
        TArgument2 value2,
        TArgument3 value3,
        TArgument4 value4,
        TArgument5 value5,
        TArgument6 value6,
        TArgument7 value7,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "",
        [CallerArgumentExpression("value3")] string paramName3 = "",
        [CallerArgumentExpression("value4")] string paramName4 = "",
        [CallerArgumentExpression("value5")] string paramName5 = "",
        [CallerArgumentExpression("value6")] string paramName6 = "",
        [CallerArgumentExpression("value7")] string paramName7 = "")
    {
        return Guards.Guard.Arguments(value1, value2, value3, value4, value5, value6, value7, paramName1, paramName2, paramName3, paramName4, paramName5, paramName6, paramName7);
    }

    public static MultipleArgumentGuard Guard<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7, TArgument8>(
        TArgument1 value1,
        TArgument2 value2,
        TArgument3 value3,
        TArgument4 value4,
        TArgument5 value5,
        TArgument6 value6,
        TArgument7 value7,
        TArgument8 value8,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "",
        [CallerArgumentExpression("value3")] string paramName3 = "",
        [CallerArgumentExpression("value4")] string paramName4 = "",
        [CallerArgumentExpression("value5")] string paramName5 = "",
        [CallerArgumentExpression("value6")] string paramName6 = "",
        [CallerArgumentExpression("value7")] string paramName7 = "",
        [CallerArgumentExpression("value8")] string paramName8 = "")
    {
        return Guards.Guard.Arguments(value1, value2, value3, value4, value5, value6, value7, value8, paramName1, paramName2, paramName3, paramName4, paramName5, paramName6, paramName7, paramName8);
    }

    public static MultipleArgumentGuard Guard<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7, TArgument8, TArgument9>(
        TArgument1 value1,
        TArgument2 value2,
        TArgument3 value3,
        TArgument4 value4,
        TArgument5 value5,
        TArgument6 value6,
        TArgument7 value7,
        TArgument8 value8,
        TArgument9 value9,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "",
        [CallerArgumentExpression("value3")] string paramName3 = "",
        [CallerArgumentExpression("value4")] string paramName4 = "",
        [CallerArgumentExpression("value5")] string paramName5 = "",
        [CallerArgumentExpression("value6")] string paramName6 = "",
        [CallerArgumentExpression("value7")] string paramName7 = "",
        [CallerArgumentExpression("value8")] string paramName8 = "",
        [CallerArgumentExpression("value9")] string paramName9 = "")
    {
        return Guards.Guard.Arguments(value1, value2, value3, value4, value5, value6, value7, value8, value9, paramName1, paramName2, paramName3, paramName4, paramName5, paramName6, paramName7, paramName8, paramName9);
    }

    public static MultipleArgumentGuard Guard<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7, TArgument8, TArgument9, TArgument10>(
        TArgument1 value1,
        TArgument2 value2,
        TArgument3 value3,
        TArgument4 value4,
        TArgument5 value5,
        TArgument6 value6,
        TArgument7 value7,
        TArgument8 value8,
        TArgument9 value9,
        TArgument10 value10,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "",
        [CallerArgumentExpression("value3")] string paramName3 = "",
        [CallerArgumentExpression("value4")] string paramName4 = "",
        [CallerArgumentExpression("value5")] string paramName5 = "",
        [CallerArgumentExpression("value6")] string paramName6 = "",
        [CallerArgumentExpression("value7")] string paramName7 = "",
        [CallerArgumentExpression("value8")] string paramName8 = "",
        [CallerArgumentExpression("value9")] string paramName9 = "",
        [CallerArgumentExpression("value10")] string paramName10 = ""
    )
    {
        return Guards.Guard.Arguments(value1, value2, value3, value4, value5, value6, value7, value8, value9, value10, paramName1, paramName2, paramName3, paramName4, paramName5, paramName6, paramName7, paramName8, paramName9, paramName10);
    }
}