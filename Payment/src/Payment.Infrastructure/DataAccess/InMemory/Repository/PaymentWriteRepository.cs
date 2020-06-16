namespace Payment.Infrastructure.DataAccess.InMemory
{
    using Payment.Domain;
    using System;
    using System.Threading.Tasks;
    using System.Linq;
    using Payment.Application.Port;
    /// <summary>
    /// Payment Repository in Memory Data
    /// </summary>
    public class PaymentWriteRepository : IPaymentWriteRepository
    {
        private readonly IDatabase _db;

        public PaymentWriteRepository(IDatabase db)
        {
            _db = db;
        }

        /// <summary>
        /// Add Payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public Task<PaymentId> AddPaymentAsync(Payment payment)
        {
            var card = GetCard(payment.Card.CardNumber) ??  new CardEntity()
            {
                Id = CardEntityNextIndex,
                CardNumber = payment.Card.CardNumber.ToString(),
                CVV = payment.Card.CVV.ToString(),
                ExpirationDate = payment.Card.ExpirationDate
            };

            var newPayment = new PaymentEntity()
            {
                Id = PaymentEntityNextIndex,
                PaymentId = payment.PaymentId.ToGuid(),
                Amount = payment.Amount.ToDecimal(),
                Currency = (byte) payment.Amount.Currency,
                CreateDate=payment.CreatedOn,
                BeneficiaryAlias = payment.BeneficiaryAlias,
                Status=(byte) payment.Status,
                Card = card
            };

            _db.Payments.Add(PaymentEntityNextIndex, newPayment);

            return Task.FromResult(payment.PaymentId);
        }

        /// <summary>
        /// Update Payment Status
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        public Task<PaymentId> UpdatePaymentStatusAsync(PaymentId paymentId, PaymentStatus paymentStatus)
        {
            var entity = GetPaymentById(paymentId);
            if (entity == null)
                throw new Exception($"update error for {paymentId.ToGuid()}");

            entity.Status = (byte) paymentStatus;

            return Task.FromResult(paymentId);
        }

        private PaymentEntity GetPaymentById(PaymentId paymentId)
        {
            return AllPaymentQuery.FirstOrDefault(p => p.PaymentId == paymentId.ToGuid());
        }

        private CardEntity GetCard(CardNumber cardNumber)
        {
            return AllCardQuery.FirstOrDefault(c=> c.CardNumber == cardNumber.ToString());
        }
        private IQueryable<PaymentEntity> AllPaymentQuery => _db.Payments.Values.AsQueryable();

        private IQueryable<CardEntity> AllCardQuery => _db.Cards.Values.AsQueryable();

        private int PaymentEntityNextIndex => (_db.Payments.Any()) ? _db.Payments.Keys.Max() + 1 : 1;

        private int CardEntityNextIndex => (_db.Cards.Any()) ? _db.Cards.Keys.Max() + 1 : 1;
    }
}
