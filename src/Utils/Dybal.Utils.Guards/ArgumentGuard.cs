namespace Dybal.Utils.Guards;

public class ArgumentGuard<TArgument>
{
    public Argument<TArgument> Argument { get; }
    public bool IsActive { get; }

    internal ArgumentGuard(Argument<TArgument> argument, bool isActive)
    {
        Argument = argument;
        IsActive = isActive;
    }
    
    public ArgumentGuard<TArgument?> If(bool condition)
    {
        if (condition)
        {
            return this!;
        }

        return new ArgumentGuard<TArgument?>(Argument!, false);
    }
}