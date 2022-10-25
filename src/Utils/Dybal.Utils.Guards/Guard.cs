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
    public static ArgumentGuard<TArgument> Argument<TArgument>(TArgument argumentValue, [CallerArgumentExpression("argumentValue")] string? argumentName = null)
    {
        return new ArgumentGuard<TArgument>(new Argument<TArgument>(argumentValue, argumentName!));
    }
    
    public static MultipleArgumentGuard Arguments<TArgument1, TArgument2>(
        TArgument1 value1,
        TArgument2 value2,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "")
    {
        var argument1 = new Argument<object?>(value1, paramName1);
        var argument2 = new Argument<object?>(value2, paramName2);

        return Arguments(new CompactList<IArgument<object?>>(argument1, argument2));
    }

    public static MultipleArgumentGuard Arguments<TArgument1, TArgument2, TArgument3>(
        TArgument1 value1,
        TArgument2 value2,
        TArgument3 value3,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "",
        [CallerArgumentExpression("value3")] string paramName3 = "")
    {
        var argument1 = new Argument<object?>(value1, paramName1);
        var argument2 = new Argument<object?>(value2, paramName2);
        var argument3 = new Argument<object?>(value3, paramName3);

        return Arguments(new CompactList<IArgument<object?>>(argument1, argument2, argument3));
    }

    public static MultipleArgumentGuard Arguments<TArgument1, TArgument2, TArgument3, TArgument4>(
        TArgument1 value1,
        TArgument2 value2,
        TArgument3 value3,
        TArgument4 value4,
        [CallerArgumentExpression("value1")] string paramName1 = "",
        [CallerArgumentExpression("value2")] string paramName2 = "",
        [CallerArgumentExpression("value3")] string paramName3 = "",
        [CallerArgumentExpression("value4")] string paramName4 = "")
    {
        var argument1 = new Argument<object?>(value1, paramName1);
        var argument2 = new Argument<object?>(value2, paramName2);
        var argument3 = new Argument<object?>(value3, paramName3);
        var argument4 = new Argument<object?>(value4, paramName4);

        return Arguments(new CompactList<IArgument<object?>>(argument1, argument2, argument3, argument4));
    }

    public static MultipleArgumentGuard Arguments<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>(
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
        var argument1 = new Argument<object?>(value1, paramName1);
        var argument2 = new Argument<object?>(value2, paramName2);
        var argument3 = new Argument<object?>(value3, paramName3);
        var argument4 = new Argument<object?>(value4, paramName4);
        var argument5 = new Argument<object?>(value5, paramName5);

        return Arguments(new CompactList<IArgument<object?>>(argument1, argument2, argument3, argument4, argument5));
    }

    public static MultipleArgumentGuard Arguments<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6>(
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
        var argument1 = new Argument<object?>(value1, paramName1);
        var argument2 = new Argument<object?>(value2, paramName2);
        var argument3 = new Argument<object?>(value3, paramName3);
        var argument4 = new Argument<object?>(value4, paramName4);
        var argument5 = new Argument<object?>(value5, paramName5);
        var argument6 = new Argument<object?>(value6, paramName6);

        return Arguments(new CompactList<IArgument<object?>>(argument1, argument2, argument3, argument4, argument5, argument6));
    }

    public static MultipleArgumentGuard Arguments<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7>(
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
        var argument1 = new Argument<object?>(value1, paramName1);
        var argument2 = new Argument<object?>(value2, paramName2);
        var argument3 = new Argument<object?>(value3, paramName3);
        var argument4 = new Argument<object?>(value4, paramName4);
        var argument5 = new Argument<object?>(value5, paramName5);
        var argument6 = new Argument<object?>(value6, paramName6);
        var argument7 = new Argument<object?>(value7, paramName7);

        return Arguments(new CompactList<IArgument<object?>>(argument1, argument2, argument3, argument4, argument5, argument6, argument7));
    }

    public static MultipleArgumentGuard Arguments<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7, TArgument8>(
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
        var argument1 = new Argument<object?>(value1, paramName1);
        var argument2 = new Argument<object?>(value2, paramName2);
        var argument3 = new Argument<object?>(value3, paramName3);
        var argument4 = new Argument<object?>(value4, paramName4);
        var argument5 = new Argument<object?>(value5, paramName5);
        var argument6 = new Argument<object?>(value6, paramName6);
        var argument7 = new Argument<object?>(value7, paramName7);
        var argument8 = new Argument<object?>(value8, paramName8);

        return Arguments(new CompactList<IArgument<object?>>(argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8));
    }

    public static MultipleArgumentGuard Arguments<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7, TArgument8, TArgument9>(
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
        var argument1 = new Argument<object?>(value1, paramName1);
        var argument2 = new Argument<object?>(value2, paramName2);
        var argument3 = new Argument<object?>(value3, paramName3);
        var argument4 = new Argument<object?>(value4, paramName4);
        var argument5 = new Argument<object?>(value5, paramName5);
        var argument6 = new Argument<object?>(value6, paramName6);
        var argument7 = new Argument<object?>(value7, paramName7);
        var argument8 = new Argument<object?>(value8, paramName8);
        var argument9 = new Argument<object?>(value9, paramName9);

        return Arguments(new CompactList<IArgument<object?>>(argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8, argument9));
    }

    public static MultipleArgumentGuard Arguments<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7, TArgument8, TArgument9, TArgument10>(
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
        var argument1 = new Argument<object?>(value1, paramName1);
        var argument2 = new Argument<object?>(value2, paramName2);
        var argument3 = new Argument<object?>(value3, paramName3);
        var argument4 = new Argument<object?>(value4, paramName4);
        var argument5 = new Argument<object?>(value5, paramName5);
        var argument6 = new Argument<object?>(value6, paramName6);
        var argument7 = new Argument<object?>(value7, paramName7);
        var argument8 = new Argument<object?>(value8, paramName8);
        var argument9 = new Argument<object?>(value9, paramName9);
        var argument10 = new Argument<object?>(value10, paramName10);

        return Arguments(new CompactList<IArgument<object?>>(argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8, argument9, argument10));
    }

    private static MultipleArgumentGuard Arguments(CompactList<IArgument<object?>> arguments)
    {
        return new MultipleArgumentGuard(arguments);
    }
}