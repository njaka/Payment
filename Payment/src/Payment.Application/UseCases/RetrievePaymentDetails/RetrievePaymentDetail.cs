namespace Payment.Application.UseCases
{
    using Payment.Application.Port;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Retrieve Payment Detail Use Case
    /// </summary>
    public class RetrievePaymentDetail : IUseCase<RetrievePaymentInput>
    {
        private readonly IPaymentReadRepository _paymentRepository;
        private readonly IRetriePaymentOutputPort _retrievePaymentOutputPort;

        public RetrievePaymentDetail(IPaymentReadRepository paymentRepository, IRetriePaymentOutputPort retrievePaymentOutputPort)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _retrievePaymentOutputPort = retrievePaymentOutputPort ?? throw new ArgumentNullException(nameof(retrievePaymentOutputPort));
        }

        public async Task Execute(RetrievePaymentInput input)
        {
            if (input is null)
            {
                _retrievePaymentOutputPort.BadRequest("Input is null");
                return;
            }

            var paymentDetail = await _paymentRepository.GetPaymenDetailById(input.PaymentId);

            if (paymentDetail is null)
            {
                _retrievePaymentOutputPort.NotFound($"Payment with Id {input.PaymentId.ToGuid()} does not exist");
                return;
            }

            _retrievePaymentOutputPort.OK(paymentDetail);
        }
    }
}
