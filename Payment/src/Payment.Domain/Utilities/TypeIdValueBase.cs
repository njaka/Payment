namespace Payment.Domain
{
    using System;
    public abstract class TypedIdValueBase
    {
        public Guid Value { get; }

        protected TypedIdValueBase(Guid value)
        {
            if (value == Guid.Empty)
                throw new InvalidOperationException("Id value cannot be empty!");
            Value = value;
        }

        public Guid ToGuid() => Value;
    }
}
