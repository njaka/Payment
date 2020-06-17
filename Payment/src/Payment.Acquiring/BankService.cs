using Payment.Application;
using Payment.Domain;
using System;
using System.Threading.Tasks;

namespace Payment.Acquiring
{
    public class BankService : IBankService
    {
        private readonly IBankClient _bankClient;
        public BankService(IBankClient bankClient)
        {
            _bankClient = bankClient ?? throw new ArgumentNullException(nameof(bankClient));
        }

        /// <summary>
        /// Submit Card Payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public async Task<BankResult> SubmitCardPaymentAsync(Domain.Payment payment)
        {
            var cardPaymentRequest = BuildBankRequest(payment);

            var cardPaymentResult = await _bankClient.CreateCardPayment(cardPaymentRequest);

            return new BankResult()
            {
                PaymentId = new PaymentId(cardPaymentResult.PaymentId),
                PaymentStatus = (PaymentStatus)cardPaymentResult.Status
            };
        }

        private CardPaymentRequest BuildBankRequest(Domain.Payment payment)
        {
            if (payment is null) throw new ArgumentNullException(nameof(payment));

            var bankRequest = new CardPaymentRequest()
            {
                PaymentId = payment.PaymentId.Value,
                Amount = payment.Amount.ToDecimal(),
                Currency = payment.Amount.Currency.ToString(),
                BeneficiaryAlias = payment.BeneficiaryAlias,
                Card = new Card()
                {
                    CardNumber = payment.Card.CardNumber.ToString(),
                    CVV = payment.Card.CVV.ToString(),
                    ExpirationDate = payment.Card.ExpirationDate.Value
                }
            };

            return bankRequest;
        }
    }

}
