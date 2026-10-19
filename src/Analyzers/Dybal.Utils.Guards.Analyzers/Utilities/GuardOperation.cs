using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;

namespace Dybal.Utils.Guards.Analyzers.Utilities;

internal static class GuardOperation
{
    public static IOperation Unwrap(IOperation operation)
    {
        while (true)
        {
            if (operation is IConversionOperation conversion)
                operation = conversion.Operand;
            else if (operation is IParenthesizedOperation parenthesized)
                operation = parenthesized.Operand;
            else
                return operation;
        }
    }

    public static IOperation UnwrapImplicit(IOperation operation)
    {
        while (true)
        {
            if (operation is IConversionOperation { IsImplicit: true } conversion)
                operation = conversion.Operand;
            else if (operation is IParenthesizedOperation parenthesized)
                operation = parenthesized.Operand;
            else
                return operation;
        }
    }

    public static bool IsBoolean(ITypeSymbol? type) =>
        type?.SpecialType == SpecialType.System_Boolean || IsNullableBoolean(type);

    public static bool IsNullableBoolean(ITypeSymbol? type) =>
        type is INamedTypeSymbol named &&
        named.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T &&
        named.TypeArguments[0].SpecialType == SpecialType.System_Boolean;

    public static bool IsComputedCondition(IOperation operation)
    {
        operation = Unwrap(operation);
        if (!IsBoolean(operation.Type))
            return false;

        return operation switch
        {
            IInvocationOperation => true,
            IBinaryOperation binary => binary.OperatorKind is
                BinaryOperatorKind.Equals or BinaryOperatorKind.NotEquals or
                BinaryOperatorKind.ObjectValueEquals or BinaryOperatorKind.ObjectValueNotEquals or
                BinaryOperatorKind.LessThan or BinaryOperatorKind.LessThanOrEqual or
                BinaryOperatorKind.GreaterThan or BinaryOperatorKind.GreaterThanOrEqual or
                BinaryOperatorKind.ConditionalAnd or BinaryOperatorKind.ConditionalOr or
                BinaryOperatorKind.And or BinaryOperatorKind.Or or BinaryOperatorKind.ExclusiveOr,
            IUnaryOperation unary => unary.OperatorKind == UnaryOperatorKind.Not,
            IIsPatternOperation => true,
            IIsTypeOperation => true,
            ILiteralOperation => true,
            IDefaultValueOperation => true,
            IConditionalOperation => true,
            ICoalesceOperation => true,
            IConditionalAccessOperation conditional => IsComputedCondition(conditional.WhenNotNull),
            IAwaitOperation => true,
            _ => false,
        };
    }

    public static bool IsEntryPoint(IInvocationOperation invocation, Compilation compilation)
    {
        var method = invocation.TargetMethod.ReducedFrom ?? invocation.TargetMethod;
        return (method.Name is "Argument" or "Arguments" &&
                IsType(method.ContainingType, compilation, "Dybal.Utils.Guards.Guard")) ||
               (method.Name == "Guard" &&
                (IsType(method.ContainingType, compilation, "Dybal.Utils.Guards.GuardProvider") ||
                 IsType(method.ContainingType, compilation, "Dybal.Utils.Guards.ObjectExtensions.GuardObjectExtensionsExtensions")));
    }

    public static IEnumerable<IOperation> GuardedArguments(IInvocationOperation invocation)
    {
        var method = (invocation.TargetMethod.ReducedFrom ?? invocation.TargetMethod).OriginalDefinition;
        var valueParameters = method.Parameters.Where(parameter => parameter.Type is ITypeParameterSymbol)
            .Select(parameter => parameter.Name).ToArray();

        foreach (var argument in invocation.Arguments)
        {
            if (argument.ArgumentKind == ArgumentKind.Explicit &&
                argument.Parameter is not null && valueParameters.Contains(argument.Parameter.Name))
                yield return argument.Value;
        }

        if (invocation.TargetMethod.ReducedFrom is not null && invocation.Instance is not null &&
            method.Parameters[0].Type is ITypeParameterSymbol)
            yield return invocation.Instance;
    }

    public static IOperation? Argument(IInvocationOperation invocation, string parameterName) =>
        invocation.Arguments.FirstOrDefault(argument => argument.Parameter?.Name == parameterName)?.Value;

    public static IOperation? ExtensionReceiver(IInvocationOperation invocation) =>
        invocation.TargetMethod.ReducedFrom is not null
            ? invocation.Instance
            : Argument(invocation, "guard") ?? Argument(invocation, "covariantGuard");

    public static bool IsGuardExtension(IInvocationOperation invocation, Compilation compilation)
    {
        var method = invocation.TargetMethod.ReducedFrom ?? invocation.TargetMethod;
        return IsType(method.ContainingType, compilation, "Dybal.Utils.Guards.ArgumentGuardExtensions");
    }

    public static IOperation? GuardedValue(IOperation? operation, Compilation compilation)
    {
        if (operation is null)
            return null;

        operation = Unwrap(operation);
        if (operation is not IInvocationOperation invocation)
            return null;

        if (IsEntryPoint(invocation, compilation))
        {
            var values = GuardedArguments(invocation).ToArray();
            return values.Length == 1 ? values[0] : null;
        }

        if (invocation.TargetMethod.Name == "Throws" &&
            IsType(invocation.TargetMethod.ContainingType, compilation, "Dybal.Utils.Guards.ArgumentGuard`1"))
            return GuardedValue(invocation.Instance, compilation);

        return IsGuardExtension(invocation, compilation)
            ? GuardedValue(ExtensionReceiver(invocation), compilation)
            : null;
    }

    public static SyntaxNode ExpressionSyntax(IOperation operation)
    {
        var syntax = operation.Syntax;
        while (syntax.Parent is ParenthesizedExpressionSyntax parenthesized)
            syntax = parenthesized;
        return syntax;
    }

    private static bool IsType(INamedTypeSymbol type, Compilation compilation, string metadataName) =>
        SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, compilation.GetTypeByMetadataName(metadataName));
}
