using Payment.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Payment.Domain.Events
{
    public class OrderPaymentPaid : Event
    {
        public static OrderPaymentPaid CreateNewOrderPaymentPaid(Guid id, decimal amount, string currency, string paymentStatus, string beneficiaryAlias)
        {
            return new OrderPaymentPaid(id, amount, currency, paymentStatus, beneficiaryAlias);
        }

        [JsonConstructor]
        protected OrderPaymentPaid(Guid id, decimal amount, string currency, string paymentStatus, string beneficiaryAlias)
        {
            Id = id;
            AggregateId = id;
            Amount = amount;
            Currency = currency;
            PaymentStatus = paymentStatus;
            BeneficiaryAlias = beneficiaryAlias;
        }

        public string BeneficiaryAlias { get; private set; }

        public Decimal Amount { get; private set; }

        public string Currency { get; private set; }

        public string PaymentStatus { get; private set; }

        public Guid Id { get; private set; }
    }
}
