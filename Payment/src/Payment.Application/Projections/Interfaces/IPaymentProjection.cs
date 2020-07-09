namespace Payment.Application.Projections
{
    using Payment.Application.UseCases;
    using Payment.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using PaymentModel = Payment.Domain.Payment;

    public interface IPaymentProjection
    {
        Task<PaymentModel> GetById(Guid paymentId);
    }
}
