

namespace Payment.Api.Controllers.V1
{
    using FluentMediator;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Payment.Api.Controllers.V1.ProcessPayment;
    using Payment.Api.Controllers.V1.RetrievePaymentDetail;
    using Payment.Application.UseCases;
    using Payment.Domain;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1.0/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RetrievePaymentPresenter _retrievePaymentPresenter;
        private readonly ProcessPaymentPresenter _processPaymentPresenter;

        public PaymentController(
            IMediator mediator,
            RetrievePaymentPresenter retrievePaymentPresenter,
            ProcessPaymentPresenter processPaymentPresenter)
        {
            _mediator = mediator;
            _retrievePaymentPresenter = retrievePaymentPresenter;
            _processPaymentPresenter = processPaymentPresenter;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessPayment(PaymentRequest request)
        {
            var input = BuildPaymentInput(request);
            await _mediator.PublishAsync(input);
            return _processPaymentPresenter.ViewModel;
        }


        [HttpGet]
        [Route("{PaymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RetrievePaymentDetailResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaymentDetail(Guid paymentId)
        {
            var input = new RetrievePaymentInput()
            {
                PaymentId = new PaymentId(paymentId)
            };

            await _mediator.PublishAsync(input);
            return _retrievePaymentPresenter.ViewModel;
        }

        private ProcessPaymentInput BuildPaymentInput(PaymentRequest request)
        {
            var input = new ProcessPaymentInput()
            {
                Card = new Domain.Card(
                        new CardNumber(request.Card.CardNumber),
                        request.Card.ExpirationDate,
                        new CCV(request.Card.CCV)),


                Amount = new Money(request.Amount, request.Currency),
                BeneficiaryAlias = request.BeneficiaryAlias
            };

            return input;
        }
    }
}