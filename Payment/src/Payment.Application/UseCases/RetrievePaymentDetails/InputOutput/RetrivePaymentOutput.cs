using System;

namespace Payment.Application.UseCases
{
    /// <summary>
    /// Retrieve Payment Detail Response
    /// </summary>
    public class RetrievePaymentDetailOutput
    {
        /// <summary>
        /// Payment Identifier
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Card
        /// </summary>
        public CardDto Card { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Beneficiary Alias
        /// </summary>
        public string BeneficiaryAlias { get; set; }

        /// <summary>
        /// Payment Status
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Payment Date
        /// </summary>
        public DateTime PaymentDate { get; set; }

    }

    /// <summary>
    /// Card
    /// </summary>
    public class CardDto
    {
       /// <summary>
        /// Card Number
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Card Expiration Date
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// CVV
        /// </summary>
        public string CVV { get; set; }
    }
}
