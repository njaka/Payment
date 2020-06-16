namespace Payment.Application.Port
{
    using Payment.Domain;
    using Payment.Application.UseCases;
    using System.Threading.Tasks;

    /// <summary>
    /// Payment Read Db Repository
    /// </summary>
    public interface IPaymentReadRepository
    {
        /// <summary>
        /// Get Payment by Id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        Task<RetrievePaymentDetailOutput> GetPaymenDetailById(PaymentId paymentId);
    }
}
