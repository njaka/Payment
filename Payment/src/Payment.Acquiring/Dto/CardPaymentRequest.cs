using System;

namespace Payment.Acquiring
{
    public class CardPaymentRequest
    {
        public Guid PaymentId { get; set; }
        /// <summary>
        /// Card
        /// </summary>
        public Card Card { get; set; }

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
    }

    public class Card
    {
        public string CardNumber { get; set; }

        /// <summary>
        /// Card Expiration Date
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// CCV
        /// </summary>
        public string CCV { get; set; }
    }
}
