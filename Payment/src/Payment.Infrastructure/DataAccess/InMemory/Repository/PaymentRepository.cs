namespace Payment.Infrastructure.DataAccess.InMemory
{
{
    using Payment.Domain;
    using System;
    using System.Threading.Tasks;
    using System.Linq;
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IDatabase _db;

        public PaymentRepository(IDatabase db)
        {
            _db = db;
        }
        public async Task<Payment> AddAsync(Payment payment)
        {
            var id = _db.Payments.Keys.Max() + 1;
           
            var card = new CardEntity()
            {
                Id = _db.Cards.Keys.Max() + 1,
                CardNumber = payment.Card.CardNumber.ToString(),
                CCV=payment.Card.CCV.ToString(),
                ExpirationDate=payment.Card.ExpirationDate
            };

            var newPayment = new PaymentEntity()
            {
                Id = id,
                PaymentId = payment.PaymentId.ToGuid(),
                Amount = payment.Amount.ToDecimal(),
                Currency = payment.Amount.GetCurrencyInt(),
                CreateDate=payment.CreatedOn,
                Status=(int) payment.Status,
                Card = card
            };

            _db.Payments.Add(id, newPayment);

            return payment;
        }

        public Task<Payment> GetPaymentByIdAsync(PaymentId paymentId)
        {
            var p = _db.Payments.Values.AsQueryable().FirstOrDefault(p => p.PaymentId == paymentId.ToGuid());
            throw new NotImplementedException();
        }

        public Task<Payment> UpdatePaymentStatusAsync(PaymentId paymentId, PaymentStatus paymentStatus)
        {
            throw new NotImplementedException();
        }
    }
}
