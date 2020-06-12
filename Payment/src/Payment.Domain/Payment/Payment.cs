namespace Payment.Domain
{
    using System;
    public class Payment
    {
        public PaymentId PaymentId { get; protected set; }

        public Card Card { get; protected set; }

        public Money Amount { get; protected set; }

        public string BeneficiaryAlias { get; private set; }

        public PaymentStatus Status { get; protected set; }

        public DateTime CreatedOn { get; protected set; }
    }
}
