namespace Payment.Application.UseCases
{
    public static class MapModelToDtoExtensions {
        public static RetrievePaymentDetailOutput ConvertFromPaymentModelToPaymentDto(this Domain.Payment paymentDetail)
        {
            return new RetrievePaymentDetailOutput()
            {
                PaymentId = paymentDetail.PaymentId.Value,
                Amount = paymentDetail.Amount.ToDecimal(),
                Currency = paymentDetail.Amount.Currency.ToString(),
                BeneficiaryAlias = paymentDetail.BeneficiaryAlias,
                PaymentDate = paymentDetail.CreatedOn,
                Status = paymentDetail.Status.ToString(),
                Card = new CardDto()
                {
                    CardNumber = paymentDetail.Card.CardNumber.CardHint,
                    CVV = paymentDetail.Card.CVV.ToString(),
                    ExpirationDate = paymentDetail.Card.ExpirationDate.Value
                }
            };
        }
    }
}
