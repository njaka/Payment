namespace Payment.Application.UseCases
{
    using Payment.Application.Port;
    using Payment.Domain;
    using System;
    using System.Threading.Tasks;


    public class RetrievePaymentDetail : IUseCase<RetrievePaymentInput>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRetriePaymentOutputPort _retrievePaymentOutputPort;

        public RetrievePaymentDetail(IPaymentRepository paymentRepository, IRetriePaymentOutputPort retrievePaymentOutputPort)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _retrievePaymentOutputPort = retrievePaymentOutputPort ?? throw new ArgumentNullException(nameof(retrievePaymentOutputPort));
        }

        public async Task Execute(RetrievePaymentInput input)
        {
            if (input is null)
                _retrievePaymentOutputPort.BadRequest("Input is null");

            var paymentDetail = await _paymentRepository.GetPaymentByIdAsync(input.PaymentId);

            if (paymentDetail == null)
                _retrievePaymentOutputPort.NotFound($"Payment with Id {input.PaymentId} does not exist");

            _retrievePaymentOutputPort.OK(paymentDetail);
        }
    }
}
