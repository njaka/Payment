using Payment.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Events
{
    public class OrderPaymentOpened : Event
    {
        public static OrderPaymentOpened CreateNewOrderPaymentOpened(Guid id, string beneficiaryAlias, Decimal amount, string currency)
        {
            return new OrderPaymentOpened(id, beneficiaryAlias, amount, currency);
        }

        private OrderPaymentOpened(Guid id, string beneficiaryAlias, Decimal amount, string currency)
        {
            Id = id;
            BeneficiaryAlias = beneficiaryAlias;
            Amount = amount;
            Currency = currency;
            AggregateId = id;
        }

        public Decimal Amount { get; private  set; }

        public string Currency { get; private  set; }

        public string BeneficiaryAlias { get; private set; }

        public Guid Id { get; private  set; }
    }
}
