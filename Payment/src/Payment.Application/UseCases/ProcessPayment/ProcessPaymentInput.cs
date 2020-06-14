using Payment.Domain;

namespace Payment.Application.UseCases
{
    public class ProcessPaymentInput
    {
        public Card Card { get; set; }

        public Money Amount { get; set; }

        public string BeneficiaryAlias { get; set; }
    }
}
