using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Payment.Domain.Wallet
{
    public class Balance : ValueObject<Balance>
    {
        public Money Amount { get; protected set; }
        public DateTime AsOf { get; protected set; }

        public static Balance CreateNewBalance(Money amount, DateTime asOf)
        {
            return new Balance(amount, asOf);
        }

        private Balance(Money amount, DateTime asOf)
        {
            Amount = amount;
            AsOf = asOf;
        }

        public Result<Balance> Add(Money moneyAdd)
        {
            SetAsOf();

            return Amount
                    .Add(moneyAdd)
                    .OnFailure(error => Result.Failure<Balance>(error))
                    .Map(money => new Balance(money, AsOf));
        }

        private void SetAsOf()
        {
            AsOf = DateTime.UtcNow;
        }

        public Result<Balance> Subtract(Money moneyAdd)
        {
            AsOf = DateTime.UtcNow;

            return Amount
                    .Subtract(moneyAdd)
                    .OnFailure(error => Result.Failure<Balance>(error))
                    .Map(money => new Balance(money, AsOf));   
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return AsOf;
        }
    }
}
