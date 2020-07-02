using Payment.Domain;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Payment.Domain.Events
{
    public class OrderPaymentCreated : Event
    {
        public static OrderPaymentCreated CreateNewOrderPayment(Guid id, string beneficiaryAlias, Decimal amount, string currency, string status, string cardNumber,
            string cvv, DateTime expirationData)
        {
            return new OrderPaymentCreated(id, beneficiaryAlias, amount, currency, status, cardNumber, cvv, expirationData);
        }

        private OrderPaymentCreated(Guid id, string beneficiaryAlias, Decimal amount, string currency, string status, string cardNumber,
            string cvv, DateTime expirationData)
        {
            Id = id;
            BeneficiaryAlias = beneficiaryAlias;
            Amount = amount;
            Currency = currency;
            Status = status;
            AggregateId = id;
            CardNumber = CardNumber;
            CVV = cvv;
            ExpirationDate = expirationData;
        }

        public string CardNumber { get; private set; }

        public string CVV { get; private set; }

        public DateTime ExpirationDate { get; private set; }

        public Decimal Amount { get; private  set; }

        public string Currency { get; private  set; }

        public string BeneficiaryAlias { get; private set; }

        public string Status { get; private set; }

        public Guid Id { get; private  set; }

        public DateTime CreatedOn { get; private set; }
        
    }
}
