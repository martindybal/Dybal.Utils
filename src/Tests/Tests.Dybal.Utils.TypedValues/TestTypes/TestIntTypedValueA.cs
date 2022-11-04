using Dybal.Utils.TypedValues;

namespace Tests.Dybal.Utils.TypedValues.TestTypes;

public record TestIntTypedValueA(int Value) : TypedValue<int>(Value);