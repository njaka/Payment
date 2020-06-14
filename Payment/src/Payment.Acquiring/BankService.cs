using Payment.Application;
using System.Threading.Tasks;

namespace Payment.Acquiring
{
    public class BankService : IBankService
    {
        public Task<BankResult> SubmitCardPaymentAsync(Domain.Payment payment)
        {
            throw new System.NotImplementedException();
        }
    }
}
