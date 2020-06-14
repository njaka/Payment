namespace Payment.Domain
{
    using System.Threading.Tasks;
    public interface IPaymentRepository
    {
        /// <summary>
        /// Add Payment Async
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<Payment> AddAsync(Payment payment);


        /// <summary>
        /// Update payment status
        /// </summary>
        /// <param name="PaymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        Task<Payment> UpdatePaymentStatusAsync(PaymentId paymentId, PaymentStatus paymentStatus);


        /// <summary>
        /// Get Payment by Id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        Task<Payment> GetPaymentByIdAsync(PaymentId paymentId);



    }
}
