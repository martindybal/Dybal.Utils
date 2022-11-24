namespace Dybal.Utils.Guards;

public interface IExceptionOverride
{
    string ArgumentName { get; }
    internal Type? ExceptionOverrideType { get; }
}