using System.Threading.Tasks;

namespace Payment.Acquiring
{
    public interface IBankClient
    {
        Task<CardPaymentResponse> CreateCardPayment(CardPaymentRequest payment);
    }
}