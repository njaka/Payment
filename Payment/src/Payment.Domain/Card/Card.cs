
namespace Payment.Domain
{
    using System;
    public class Card : ValueObject
    {
        public CardNumber CardNumber { get; protected set; }

        public DateTime ExpirationDate { get; protected set; }

        public CVV CVV { get; protected set; }

        public Card(CardNumber cardNumber, DateTime expirationDate, CVV ccv)
        {
            this.CheckRule(new ExpirationDateShouldBeGreaterThanDatetimeNow(expirationDate));

            this.CardNumber = cardNumber;
            this.ExpirationDate = expirationDate;
            this.CVV = ccv;
        }
    }
}
