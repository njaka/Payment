namespace Payment.Domain
{
    using System;
    public class Payment
    {
        public PaymentId PaymentId { get; protected set; }

        public Card Card { get; protected set; }

        public Money Amount { get; protected set; }

        public string BeneficiaryAlias { get; protected set; }

        public PaymentStatus Status { get; protected set; }

        public DateTime CreatedOn { get; protected set; }


        public static Payment CreateCardPayment(Card card, Money amount, string beneficiaryAlias)
        {
            return new Payment(card, amount, beneficiaryAlias);
        }

        private Payment(Card card, Money amount, string beneficiaryAlias)
        {
            this.PaymentId = new PaymentId(Guid.NewGuid());
            this.Card = card;
            this.Amount = amount;
            this.BeneficiaryAlias = beneficiaryAlias;
            this.Status = PaymentStatus.Pending;
            this.CreatedOn = DateTime.Now;
        }

        public void UpdateStatus(PaymentStatus paymentStatus)
        {
            this.Status = paymentStatus;
        }
    }
}
