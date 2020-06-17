
namespace Payment.Domain
{
    using System;
    public class Card : ValueObject
    {
        public CardNumber CardNumber { get; protected set; }

        public ExpiryDate ExpirationDate { get; protected set; }

        public CVV CVV { get; protected set; }

        public Card(CardNumber cardNumber, ExpiryDate expirationDate, CVV ccv)
        {
            this.CardNumber = cardNumber;
            this.ExpirationDate = expirationDate;
            this.CVV = ccv;
        }
    }
}
