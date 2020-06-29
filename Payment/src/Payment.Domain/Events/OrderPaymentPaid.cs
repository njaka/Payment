using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Events
{
    public class OrderPaymentPaid : Event
    {
        public OrderPaymentPaid(Guid id, string beneficiaryAlias, Decimal amount, int currency)
        {
            Id = id;
            BeneficiaryAlias = beneficiaryAlias;
            Amount = amount;
            Currency = currency;
            AggregateId = id;
        }

        public Decimal Amount { get; private set; }

        public int Currency { get; private set; }

        public string BeneficiaryAlias { get; private set; }

        public Guid Id { get; private set; }
    }
}
