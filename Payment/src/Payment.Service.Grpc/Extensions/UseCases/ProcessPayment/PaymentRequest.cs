using System;
using System.ComponentModel.DataAnnotations;

namespace Payment.Service.Grpc.ProcessPayment
{
    public class PaymentRequest
    {
        /// <summary>
        /// Card
        /// </summary>
        public Card Card { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [Required]
        public string Currency { get; set; }

        /// <summary>
        /// Beneficiary Alias
        /// </summary>
        [Required]
        public string BeneficiaryAlias { get; set; }
    }

    public class Card
    {
        [Required]
        public string CardNumber { get; set; }

        /// <summary>
        /// Card Expiration Date
        /// </summary>
        [Required]
        public string ExpirationDate { get; set; }

        /// <summary
        /// CVV
        /// </summary>
        [Required]
        public string CVV { get; set; }
    }
}
