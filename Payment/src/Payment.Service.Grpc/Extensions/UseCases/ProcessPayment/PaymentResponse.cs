
namespace Payment.Service.Grpc
{
    using Payment.Application.UseCases;
    using System;

    /// <summary>
    /// Process Payment Response
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// Payment Identifier
        /// </summary>
        public Guid PaymentId { get; private set; }

        /// <summary>
        /// Payment Status
        /// </summary>
        public int PaymentStatus { get; private set; }

        public PaymentResponse(ProcessPaymentOutput output)
        {
            PaymentId = output.PaymentId.Value;
            PaymentStatus = (int)output.PaymentStatus;
        }
    }
}
