namespace Payment.Application
{
    using Payment.Domain;
    public class BankResult
    {
        public PaymentId PaymentId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }
}
