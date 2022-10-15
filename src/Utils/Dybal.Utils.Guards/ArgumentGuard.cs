namespace Dybal.Utils.Guards;

public class ArgumentGuard<TArgument> : IArgumentGuard<TArgument>
{
    public Argument<TArgument> Argument { get; }
    public bool IsActive { get; }

    TArgument IArgumentGuard<TArgument>.ArgumentValue => Argument.Value;

    string IArgumentGuard<TArgument>.ArgumentName => Argument.Name;

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