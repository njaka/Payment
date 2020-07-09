using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Payment.Domain
{
    public class Money : ValueObject<Money>
    {
        public static Money CreateNewMoneyDollars(Decimal amount)
        {
            return new Money(amount, Currency.USD);
        }

        public static Money CreateNewMoney(Decimal amount, string currency)
        {
            return new Money(amount, currency);
        }

        public Money(Decimal amount, string currency)
        {
            this.CheckRule(new AmountShouldBePositive(amount));
            this.CheckRule(new CurrencyShouldEuroOrDollarOrPound(currency));

            this.Amount = amount;
            this.Currency = (Currency)Enum.Parse(typeof(Currency), currency);
        }

        public Money(Decimal amount, Currency currency)
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
