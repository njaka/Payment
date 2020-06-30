﻿namespace Payment.Application.UseCases
{
    using MediatR;
    using Payment.Application.Port;
    using Payment.Domain;
    using Payment.Domain.Events;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Process Payment Use Case
    /// </summary>
    public class ProcessCardPayment : IUseCase<ProcessPaymentInput>
    {
        private readonly IMediator _mediator;
        private readonly IProcessPaymentOutputPort _paymentOutputPort;
        private readonly IBankService _bankService;

        public ProcessCardPayment(IMediator mediator, IProcessPaymentOutputPort paymentOutputPort, IBankService bankService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _bankService = bankService ?? throw new ArgumentNullException(nameof(bankService));
            _paymentOutputPort = paymentOutputPort ?? throw new ArgumentNullException(nameof(paymentOutputPort));
        }

        public async Task Execute(ProcessPaymentInput input)
        {
            if (input is null)
            {
                _paymentOutputPort.BadRequest("input is null");
                return;
            }

            var payment = Payment.CreateNewCardPayment(input.Card, input.Amount, input.BeneficiaryAlias);

            var bankResult = await _bankService.SubmitCardPaymentAsync(payment).ConfigureAwait(false);

            payment.UpdateStatus(bankResult.PaymentStatus);

            _paymentOutputPort.OK(BuildOutput(bankResult));
        }


        private ProcessPaymentOutput BuildOutput(BankResult bankResult)
        {
            return new ProcessPaymentOutput()
            {
                PaymentId = bankResult.PaymentId,
                PaymentStatus = bankResult.PaymentStatus
            };
        }
    }
}
