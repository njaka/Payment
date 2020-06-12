namespace Payment.Domain
{
    using System;
    public class PaymentId : TypedIdValueBase
    {
        public PaymentId(Guid value) : base(value)
        {

        }
    }
}
