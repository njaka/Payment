using System;

namespace Payment.Domain
{
    public interface IPaymentBuilder
    {
        Payment Build();

        IPaymentBuilder WithBeneficiary(string beneficiaryAlias);

        IPaymentBuilder WithCard(Card card);

        IPaymentBuilder WithAmount(Money amout);

        IPaymentBuilder CreatedOn(DateTime createdOn);
    }
}
