using System;

namespace Payment.Domain
{
    public class Money : ValueObject
    {
        public Money(Decimal amount, Currency currency)
        {
            this.CheckRule(new AmountShouldBePositive(amount));
            this.CheckRule(new CurrencyShouldEuroOrDollarOrPound(currency));

            this.Amount = amount;
            this.Currency = currency;
        }

        private Decimal Amount { get; }

        private Currency Currency { get; }
    }
}
