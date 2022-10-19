namespace Dybal.Utils.Guards;

public class ExceptionNotRegisteredException : ArgumentException
{
    public ExceptionNotRegisteredException(Type exceptionType) : base($"Exception {exceptionType.FullName} was not registered. Use ThrowHelper.Register({exceptionType.Name}Factory).")
    {
    }
}