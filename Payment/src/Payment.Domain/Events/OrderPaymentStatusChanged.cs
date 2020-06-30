using Payment.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Events
{
    public class OrderPaymentStatusChanged : Event
    {
        public static OrderPaymentStatusChanged CreateNewOrderPaymentStatusChanged(Guid id, string paymentStatus, string beneficiaryAlias)
        {
            return new OrderPaymentStatusChanged(id, paymentStatus, beneficiaryAlias);
        }
        
        private OrderPaymentStatusChanged(Guid id, string paymentStatus, string beneficiaryAlias)
        {
            Id = id;
            AggregateId = id;
            PaymentStatus = paymentStatus;
            BeneficiaryAlias = beneficiaryAlias;
        }

        public string BeneficiaryAlias { get; private set; }

        public string PaymentStatus { get; private set; }

        public Guid Id { get; private set; }
    }
}
