namespace Dybal.Utils.TypedValues;

public abstract record TypedId<TTypedId> : TypedValue<Guid>
    where TTypedId : TypedId<TTypedId>
{
    protected TypedId(Guid value) : base(value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Value cannot be an empty GUID.", nameof(value));
        }
    }
}