namespace Dybal.Utils.Guards;

public class ExceptionNotRegisteredException : ArgumentException
{
    public ExceptionNotRegisteredException(Type exceptionType) : base($"Exception {exceptionType.FullName} was not registered. Use {nameof(ThrowHelper)}.{nameof(ThrowHelper.TryRegister)}({exceptionType.Name}Factory).")
    {
    }
}