using CSharpFunctionalExtensions;
using Payment.Domain;
using Payment.Domain.DomainServices;
using System;
using Xunit;

namespace Payment.Tests
{
    public class Money_Spec
    {
        private static readonly ICurrencyLookup CurrencyLookup =
            new CurrencyLookup();

        [Fact]
        public void Two_of_same_amount_should_be_equal()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", CurrencyLookup);
            var secondAmount = Money.FromDecimal(5, "EUR", CurrencyLookup);

            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Two_of_same_amount_but_different_Currencies_should_not_be_equal()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", CurrencyLookup);
            var secondAmount = Money.FromDecimal(5, "USD", CurrencyLookup);

            Assert.NotEqual(firstAmount, secondAmount);
        }


        [Fact]
        public void FromString_and_FromDecimal_should_be_equal()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", CurrencyLookup);
            var secondAmount = Money.FromString("5", "EUR", CurrencyLookup);

            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Sum_of_money_gives_full_amount()
        {
            var coin1 = Money.FromDecimal(1, "EUR", CurrencyLookup);
            var coin2 = Money.FromDecimal(2, "EUR", CurrencyLookup);
            var coin3 = Money.FromDecimal(2, "EUR", CurrencyLookup);

            var banknote = Money.FromDecimal(5, "EUR", CurrencyLookup);

            Assert.Equal(banknote, coin1 + coin2 + coin3);
        }

        [Fact]
        public void Unused_currency_should_not_be_allowed()
        {
            Assert.Throws<ValueObjectException>(() =>
                Money.FromDecimal(100, "DEM", CurrencyLookup)
            );
        }

        [Fact]
        public void Unknown_currency_should_not_be_allowed()
        {
            Assert.Throws<ValueObjectException>(() =>
                Money.FromDecimal(100, "WHAT?", CurrencyLookup)
            );
        }

        [Fact]
        public void Throw_when_too_many_decimal_places()
        {
            Assert.Throws<ValueObjectException>(() =>
                Money.FromDecimal(100.123m, "EUR", CurrencyLookup)
            );
        }

        [Fact]
        public void Throws_on_adding_different_currencies()
        {
            var firstAmount = Money.FromDecimal(5, "USD", CurrencyLookup);
            var secondAmount = Money.FromDecimal(5, "EUR", CurrencyLookup);

            Assert.Throws<ResultFailureException>(() => 
                firstAmount + secondAmount
            );
        }

        [Fact]
        public void Throws_on_substracting_different_currencies()
        {
            var firstAmount = Money.FromDecimal(5, "USD", CurrencyLookup);
            var secondAmount = Money.FromDecimal(5, "EUR", CurrencyLookup);

            Assert.Throws<ResultFailureException>(() =>
                firstAmount - secondAmount
            );
        }
    }
}
