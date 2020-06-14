using System;

namespace Payment.Api.Controllers.V1
{
    /// <summary>
    /// Retrieve Payment Detail Response
    /// </summary>
    public class RetrievePaymentDetailResponse
    {

        public RetrievePaymentDetailResponse(Domain.Payment output)
        {
            if (output is null) throw new ArgumentNullException(nameof(Domain.Payment));

            PaymentId = output.PaymentId.Value;
            Card = new Card(output.Card);
            Amount = output.Amount.ToDecimal();
            Currency = output.Amount.Currency.ToString();
            BeneficiaryAlias = output.BeneficiaryAlias;
            Status = (int)output.Status;
            PaymentDate = output.CreatedOn;
        }

        /// <summary>
        /// Payment Identifier
        /// </summary>
        public Guid PaymentId { get; protected set; }

        /// <summary>
        /// Card
        /// </summary>
        public Card Card { get; protected set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; protected set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; protected set; }

        /// <summary>
        /// Beneficiary Alias
        /// </summary>
        public string BeneficiaryAlias { get; protected set; }

        /// <summary>
        /// Payment Status
        /// </summary>
        public int Status { get; protected set; }

        /// <summary>
        /// Payment Date
        /// </summary>
        public DateTime PaymentDate { get; protected set; }

    }

    /// <summary>
    /// Card
    /// </summary>
    public class Card
    {
        public Card(Domain.Card card)
        {
            if (card is null) throw new ArgumentNullException(nameof(Domain.Card));

            this.CardNumber = card.CardNumber.ToString();
            this.ExpirationDate = card.ExpirationDate;
            this.CCV = card.CCV.ToString(); ;
        }

        /// <summary>
        /// Card Number
        /// </summary>
        public string CardNumber { get; protected set; }

        /// <summary>
        /// Card Expiration Date
        /// </summary>
        public DateTime ExpirationDate { get; protected set; }

        /// <summary>
        /// CCV
        /// </summary>
        public string CCV { get; protected set; }
    }
}
