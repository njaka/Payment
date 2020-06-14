namespace Payment.Infrastructure
{
    using Payment.Domain;
    using System;
    using System.Threading.Tasks;
    public class PaymentRepository : IPaymentRepository
    {
        public Task<Payment> AddAsync(Payment payment)
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetPaymentByIdAsync(PaymentId paymentId)
        {
            throw new NotImplementedException();
        }

        public Task<Payment> UpdatePaymentStatusAsync(PaymentId paymentId, PaymentStatus paymentStatus)
        {
            throw new NotImplementedException();
        }
    }
}
