using System;
using System.ComponentModel.DataAnnotations;

namespace Payment.Api.Controllers.V1.ProcessPayment
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
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// CCV
        /// </summary>
        [Required]
        public string CCV { get; set; }
    }
}
