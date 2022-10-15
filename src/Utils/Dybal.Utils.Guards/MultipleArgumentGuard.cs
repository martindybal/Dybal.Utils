namespace Dybal.Utils.Guards;

public record struct MultipleArgumentGuard
{
    public Argument<object?>[] Arguments { get; }
    public bool IsActive { get; private set; }

    internal MultipleArgumentGuard(Argument<object?>[] arguments, bool isActive)
    {
        Arguments = arguments;
        IsActive = isActive;
    }

    public MultipleArgumentGuard If(bool condition)
    {
        if (!condition)
        {
            IsActive = false;
        }
        return this;
    }
}