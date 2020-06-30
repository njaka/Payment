using Payment.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Events
{
    public class OrderPaymentCreated : Event
    {
        public static OrderPaymentCreated CreateNewOrderPayment(Guid id, string beneficiaryAlias, Decimal amount, string currency, string status)
        {
            return new OrderPaymentCreated(id, beneficiaryAlias, amount, currency, status);
        }

        private OrderPaymentCreated(Guid id, string beneficiaryAlias, Decimal amount, string currency, string status)
        {
            Id = id;
            BeneficiaryAlias = beneficiaryAlias;
            Amount = amount;
            Currency = currency;
            Status = status;
            AggregateId = id;
        }

        public Decimal Amount { get; private  set; }

        public string Currency { get; private  set; }

        public string BeneficiaryAlias { get; private set; }

        public string Status { get; private set; }

        public Guid Id { get; private  set; }
    }
}
