namespace Payment.Infrastructure.DataAccess.InMemory
{
    using Payment.Domain;
    using System;
    using System.Threading.Tasks;
    using System.Linq;
    using Payment.Application.Port;
    using Payment.Application.UseCases;

    /// <summary>
    /// Payment Read Repository in Memory Data
    /// </summary>
    public class PaymentReadRepository:IPaymentReadRepository
    {
        private readonly IDatabase _db;

        public PaymentReadRepository(IDatabase db)
        {
            _db = db;
        }

        /// <summary>
        /// Get Payment By Identifiers
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        Task<RetrievePaymentDetailOutput> IPaymentReadRepository.GetPaymenDetailById(PaymentId paymentId)
        {
            RetrievePaymentDetailOutput result = null;

            var p = AllPaymentQuery.FirstOrDefault(x=>x.PaymentId == paymentId.ToGuid());

            if (p is null)
                return Task.FromResult(result);

            result = new RetrievePaymentDetailOutput()
            {
                PaymentId = p.PaymentId,
                Card = new CardDto()
                {
                    CardNumber = p.Card.CardNumber,
                    ExpirationDate = p.Card.ExpirationDate,
                    CVV = p.Card.CVV
                },
                Amount = p.Amount,
                Currency = ((Currency)p.Currency).ToString() ,
                BeneficiaryAlias = p.BeneficiaryAlias,
                Status = p.Status,
                PaymentDate =p.CreateDate,

            };

            return Task.FromResult(result);
        }

        private IQueryable<PaymentEntity> AllPaymentQuery => _db.Payments.Values.AsQueryable();

    }
}
