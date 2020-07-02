namespace Payment.Application
{
    using Payment.Application.UseCases;
    using Payment.Domain;
    public class BankResult
    {
        public PaymentId PaymentId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }

    public static class BankResultExtensions
    {
        public static ProcessPaymentOutput BuildOutput(this BankResult bankResult)
        {
            return new ProcessPaymentOutput()
            {
                PaymentId = bankResult.PaymentId,
                PaymentStatus = bankResult.PaymentStatus
            };
        }
    }
}
