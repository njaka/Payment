namespace Payment.Application.UseCases
{
    using Payment.Domain;

    public class ProcessPaymentOutput
    {
        public PaymentId PaymentId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }
}
