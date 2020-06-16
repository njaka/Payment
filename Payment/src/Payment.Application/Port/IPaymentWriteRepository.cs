namespace Payment.Application.Port
{
    using System.Threading.Tasks;
    using Payment.Domain;

    /// <summary>
    /// Paymetn Write DB Repository
    /// </summary>
    public interface IPaymentWriteRepository
    {
        /// <summary>
        /// Add Payment Async
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<PaymentId> AddPaymentAsync(Payment payment);


        /// <summary>
        /// Update payment status
        /// </summary>
        /// <param name="PaymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        Task<PaymentId> UpdatePaymentStatusAsync(PaymentId paymentId, PaymentStatus paymentStatus);
    }
}
