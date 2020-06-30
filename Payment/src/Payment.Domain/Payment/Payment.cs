namespace Payment.Domain
{
    using global::Payment.Domain.Events;
    using System;
    public class Payment
    {
        public PaymentId PaymentId { get; protected set; }

        public Card Card { get; protected set; }

        public Money Amount { get; protected set; }

        public string BeneficiaryAlias { get; protected set; }

        public PaymentStatus Status { get; protected set; }

        public DateTime CreatedOn { get; protected set; }


        public static Payment CreateNewCardPayment(Card card, Money amount, string beneficiaryAlias)
        {
            return new Payment(card, amount, beneficiaryAlias);
        }

        public void UpdateStatus(PaymentStatus status)
        {
            this.Status = status;

            DomainEvents
                    .Raise(
                            OrderPaymentStatusChanged
                                    .CreateNewOrderPaymentStatusChanged(
                                                            this.PaymentId.Value,
                                                            this.Status.ToString(),
                                                            this.BeneficiaryAlias
                                                           )
                           );
        }

        private Payment(Card card, Money amount, string beneficiaryAlias)
        {
            this.PaymentId = new PaymentId(Guid.NewGuid());
            this.Card = card;
            this.Amount = amount;
            this.BeneficiaryAlias = beneficiaryAlias;
            this.Status = PaymentStatus.Pending;
            this.CreatedOn = DateTime.Now;

            DomainEvents
                    .Raise(
                            OrderPaymentCreated
                                    .CreateNewOrderPayment(
                                                            this.PaymentId.Value,
                                                            this.BeneficiaryAlias,
                                                            this.Amount.Amount,
                                                            this.Amount.Currency.ToString(),
                                                            this.Status.ToString()
                                                           )
                           );
        }
    }
}
