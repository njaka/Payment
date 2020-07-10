

namespace Payment.Api.Controllers.V1
{
    using FluentMediator;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Payment.Api.Controllers.V1.ProcessPayment;
    using Payment.Api.Controllers.V1.RetrievePaymentDetail;
    using Payment.Application.UseCases;
    using Payment.Domain;
    using Payment.Domain.DomainServices;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Payment Controller
    /// </summary>
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

        /// <summary>
        /// Process payment
        /// </summary>
        /// <param name="paymentRequest">payment Request</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessPayment(PaymentRequest paymentRequest)
        {
            var input = BuildPaymentInput(paymentRequest);
            await _mediator.PublishAsync(input);
            return _processPaymentPresenter.ViewModel;
        }


        /// <summary>
        /// Retrieve Payment Detail
        /// </summary>
        /// <param name="paymentId">payment Identifier</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{paymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RetrievePaymentDetailOutput))]
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
                        new ExpiryDate(request.Card.ExpirationDate),
                        new CVV(request.Card.CVV)),


                Amount = new Money(request.Amount, request.Currency, new CurrencyLookup()),
                BeneficiaryAlias = request.BeneficiaryAlias
            };

            return input;
        }
    }
}