namespace Dybal.Utils.Guards.Analyzers.Utilities;

public static class DiagnosticIds
{
    // Reserved
    public const string InternalAnalyzerError = "Guards01";

    public const string ArgumentGuard_UseEqualToInsteadOfEquals = "Guards02";
    public const string MultipleArgumentGuard_DoNotUseEquals = "Guards03";
    public const string Guard_DoNotGuardComputedCondition = "Guards04";
    public const string ArgumentGuard_UseBooleanGuard = "Guards05";
    public const string ArgumentGuard_DoNotGuardComputedSize = "Guards06";
}
