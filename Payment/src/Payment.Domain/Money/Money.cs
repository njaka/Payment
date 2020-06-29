using System;
using System.Collections.Generic;

namespace Payment.Domain
{
    public class Money : ValueObject<Money>
    {
        public Money(Decimal amount, string currency)
        {
            this.CheckRule(new AmountShouldBePositive(amount));
            this.CheckRule(new CurrencyShouldEuroOrDollarOrPound(currency));

            this.Amount = amount;
            this.Currency = (Currency)Enum.Parse(typeof(Currency), currency);
        }

        public decimal ToDecimal()
        {
            return Amount;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public Decimal Amount { get; }

        public Currency Currency { get; }
    }
}
