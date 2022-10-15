namespace Dybal.Utils.Guards;

public class MultipleArgumentGuard
{
    public Argument<object?>[] Arguments { get; }
    public bool IsActive { get; }

    internal MultipleArgumentGuard(Argument<object?>[] arguments, bool isActive)
    {
        Arguments = arguments;
        IsActive = isActive;
    }
}