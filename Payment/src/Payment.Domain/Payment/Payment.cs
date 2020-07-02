namespace Payment.Domain
{
    using global::Payment.Domain.Events;
    using global::Payment.Domain.Utilities;
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class Payment : ModelBase
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

        public Payment UpdateStatus(PaymentStatus status)
        {
            this.Status = status;

            RegisterEvent(
                            OrderPaymentStatusChanged
                                    .CreateNewOrderPaymentStatusChanged(
                                                            this.PaymentId.Value,
                                                            this.Status.ToString(),
                                                            this.BeneficiaryAlias
                                                           )
                           );
            return this;
        }

        public Payment RaiseEvents()
        {
            DomainEvents.DipatchEvents(_events);
            _events.Clear();
            return this;
        }

        private Payment(Card card, Money amount, string beneficiaryAlias) : base()
        {
            this.PaymentId = new PaymentId(Guid.NewGuid());
            this.Card = card;
            this.Amount = amount;
            this.BeneficiaryAlias = beneficiaryAlias;
            this.Status = PaymentStatus.Pending;
            this.CreatedOn = DateTime.Now;

            RegisterEvent(
                            OrderPaymentCreated
                                    .CreateNewOrderPayment(
                                                            this.PaymentId.Value,
                                                            this.BeneficiaryAlias,
                                                            this.Amount.Amount,
                                                            this.Amount.Currency.ToString(),
                                                            this.Status.ToString(),
                                                            this.Card.CardNumber.CardHint,
                                                            this.Card.CVV.ToString(),
                                                            this.Card.ExpirationDate.Value
                                                           )
                           );
        }
    }
}
