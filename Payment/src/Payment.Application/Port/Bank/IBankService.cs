namespace Payment.Application
{

    using Payment.Domain;
    using System.Threading.Tasks;

    public interface IBankService
    {
        Task<BankResult> SubmitCardPaymentAsync(Payment payment);
    }
}
