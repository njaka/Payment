using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Payment.Domain
{
    public class Money : ValueObject<Money>
    {

        public Money(Decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
        {
            this.CheckRule(new CurrencyShouldBeSpecified(currencyCode));
            this.CheckRule(new AmountShouldBePositive(amount));

            var currency = currencyLookup.FindCurrency(currencyCode);

            this.CheckRule(new CurrencySouldBeInUse(currencyCode, currency.InUse));
            this.CheckRule(new AmountShouldBeDecimalPlacesLessThanCurrencypublic(amount, currency.DecimalPlaces, currencyCode));
            
            this.Amount = amount;
            this.Currency = currency;
        }

        protected Money(Decimal amount, Currency currency)
        {
            this.CheckRule(new AmountShouldBePositive(amount));

            this.Amount = amount;
            this.Currency = currency;
        }

        public Result<Money> Add(Money moneyAdd)
        {
            if (moneyAdd.Currency != Currency)
                return Result.Failure<Money>("Different Corrency");
            
            return Result.Ok(new Money(Amount + moneyAdd.Amount, Currency));
        }

        public Result<Money> Subtract(Money moneyAdd)
        {
            if (moneyAdd.Currency != Currency)
                return Result.Failure<Money>("Different Corrency");

            return Result.Ok(new Money(Amount - moneyAdd.Amount, Currency));
        }

        public static Money FromDecimal(
            decimal amount,
            string currency,
            ICurrencyLookup currencyLookup)
            => new Money(amount, currency, currencyLookup);

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
