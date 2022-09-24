using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class Guard
{
    public static IGuardComparison<T> Greater<T>(T value, [CallerArgumentExpression("value")] string? paramName = null)
        where T : IComparable<T>
    {
        return new GuardComparison<T>("Greater", value, paramName, compareResult => compareResult > 0);
    }

    public static IGuardComparison<T> GreaterOrEqual<T>(T value, [CallerArgumentExpression("value")] string? paramName = null)
        where T : IComparable<T>
    {
        return new GuardComparison<T>("Greater or equal", value, paramName, compareResult => compareResult >= 0);
    }

    public static IGuardComparison<T> Less<T>(T value, [CallerArgumentExpression("value")] string? paramName = null)
        where T : IComparable<T>
    {
        return new GuardComparison<T>("Less", value, paramName, compareResult => compareResult < 0);
    }

    public static IGuardComparison<T> LessOrEqual<T>(T value, [CallerArgumentExpression("value")] string? paramName = null)
        where T : IComparable<T>
    {
        return new GuardComparison<T>("Less or equal", value, paramName, compareResult => compareResult <= 0);
    }

    public static void Than<T>(this IGuardComparison<T> leftOperandComparison, T value, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
        where T : IComparable<T>
    {
        var leftOperandValue = leftOperandComparison.Value;
        var rightOperandValue = value;

        if (!leftOperandComparison.VerifyCompareResult(leftOperandValue.CompareTo(rightOperandValue)))
        {
            var leftOperandParamName = leftOperandComparison.ParamName;
            var rightOperandParamName = paramName;
            var defaultMessage = $"Value of parameter '{leftOperandParamName}' ({leftOperandValue}) must be {leftOperandComparison.Name.ToLower()} than value of parameter '{rightOperandParamName}' ({rightOperandValue}).";
            throw new ArgumentException(message ?? defaultMessage, rightOperandParamName);
        }
    }

    public static void ThanIf<T>(this IGuardComparison<T> leftOperandComparison, T value, bool condition, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
        where T : IComparable<T>
    {
        if (condition)
        {
            leftOperandComparison.Than(value, message, paramName);
        }
    }

    public static void ThanIf<T>(this IGuardComparison<T> leftOperandComparison, T? value, bool condition, string? message = null, [CallerArgumentExpression("value")] string? paramName = null)
        where T : struct, IComparable<T>
    {
        if (condition)
        {
            NotNull(value, paramName: paramName);
            leftOperandComparison.Than(value.Value, message, paramName);
        }
    }

    public interface IGuardComparison<out T>
        where T : IComparable<T>
    {
        internal string Name { get; }
        internal  string ParamName { get; }
        internal T Value { get; }
        internal bool VerifyCompareResult(int compareResult);
    }

    private class GuardComparison<T> : IGuardComparison<T>
        where T : IComparable<T>
    {
        private readonly Func<int, bool> verifyCompareResult;
        private readonly string name;
        private readonly T value;
        private readonly string paramName;

        string IGuardComparison<T>.Name => name;
        T IGuardComparison<T>.Value => value;
        string IGuardComparison<T>.ParamName => paramName;

        public GuardComparison(string name, T value, string? paramName, Func<int, bool> verifyCompareResult)
        {
            NotNullOrWhiteSpace(name);
            NotNullOrWhiteSpace(paramName);

            this.name = name;
            this.value = value;
            this.paramName = paramName;
            this.verifyCompareResult = verifyCompareResult;
        }

        bool IGuardComparison<T>.VerifyCompareResult(int compareResult)
        {
            return verifyCompareResult(compareResult);
        }
    }
}