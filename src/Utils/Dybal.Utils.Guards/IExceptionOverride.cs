namespace Dybal.Utils.Guards;

public interface IExceptionOverride
{
    internal string ArgumentName { get; }
    internal Type? ExceptionOverrideType { get; }
}