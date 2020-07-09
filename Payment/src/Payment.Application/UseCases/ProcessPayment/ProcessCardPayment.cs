namespace Payment.Application.UseCases
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using Payment.Application.Port;
    using Payment.Domain;
    using Payment.Domain.Events;
    using Payment.Domain.Wallet;
    using System;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Process Payment Use Case
    /// </summary>
    public class ProcessCardPayment : IUseCase<ProcessPaymentInput>
    {
        private readonly IProcessPaymentOutputPort _paymentOutputPort;
        private readonly IBankService _bankService;

        public ProcessCardPayment(IProcessPaymentOutputPort paymentOutputPort, IBankService bankService)
        {
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
            
            Payment payment = Payment
                            .CreateNewCardPayment
                            (
                                input.Card, 
                                input.Amount, 
                                input.BeneficiaryAlias
                            )
                            .RaiseEvents();

            BankResult bankResult = await _bankService
                                        .SubmitCardPaymentAsync(payment)
                                        .ConfigureAwait(false);

            if (bankResult.PaymentStatus.Equals(PaymentStatus.Succeed))
            {
                payment
                    .Paid()
                    .RaiseEvents();
            }
            

            _paymentOutputPort.OK(bankResult.BuildOutput());
        }
    }
}
