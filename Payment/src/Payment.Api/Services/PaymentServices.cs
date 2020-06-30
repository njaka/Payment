using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using FluentMediator;
using Payment.Api.Controllers.V1;
using Payment.Api.Controllers.V1.RetrievePaymentDetail;
using Payment.Application.UseCases;
using Payment.Domain;

namespace Payment.Api.Services
{
    /// <summary>
    /// Payment Services
    /// </summary>
    public class PaymentServices :  Payments.PaymentsBase
    {
        private readonly IMediator _mediator;
        private readonly RetrievePaymentPresenter _retrievePaymentPresenter;
        private readonly ProcessPaymentPresenter _processPaymentPresenter;

        /// <summary>
        /// Payment GRPC
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="retrievePaymentPresenter"></param>
        /// <param name="processPaymentPresenter"></param>
        public PaymentServices(IMediator mediator,
            RetrievePaymentPresenter retrievePaymentPresenter,
            ProcessPaymentPresenter processPaymentPresenter)
        {
            _mediator = mediator;
            _retrievePaymentPresenter = retrievePaymentPresenter;
            _processPaymentPresenter = processPaymentPresenter;
        }

        public override async Task<PaymentViewModelReply> Process(PaymentViewModelRequest request, ServerCallContext context)
        {
            await _mediator.PublishAsync(BuildPaymentInput(request));
            
            return new PaymentViewModelReply
            {
                Message = "Ok"
            };
        }

        private ProcessPaymentInput BuildPaymentInput(PaymentViewModelRequest request)
        {
            var input = new ProcessPaymentInput()
            {
                Card = new Domain.Card(
                        new CardNumber(request.Card.CardNumber),
                        new ExpiryDate(request.Card.ExpirationDate),
                        new CVV(request.Card.Cvv)),


                Amount = new Money(Convert.ToDecimal(request.Amount), request.Currency),
                BeneficiaryAlias = request.BeneficiaryAlias
            };

            return input;
        }
    }
}
