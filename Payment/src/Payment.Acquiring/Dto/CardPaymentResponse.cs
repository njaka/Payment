using System;

namespace Payment.Acquiring
{
    public class CardPaymentResponse
    {
        public Guid PaymentId { get; set; }

        public int Status { get; set; }
    }
}
