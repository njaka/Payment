using System;

namespace Payment.Domain
{
    public class Money : ValueObject
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

        public int GetCurrencyInt()
        {
            return (int)Currency;
        }

        public Decimal Amount { get; }

        public Currency Currency { get; }
    }
}
