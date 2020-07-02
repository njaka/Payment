namespace Payment.Application.UseCases.RetriveBalance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Payment.Application.Port;
    using Payment.Application.Projections;

    public class RetrieveBalance : IUseCase<RetriveBalanceInput>
    {
        private readonly IPaymentProjection _paymentProjection;
        private readonly IRetriveBalanceOutputPort _retriveBalanceOutputPort;

        public RetrieveBalance(IPaymentProjection paymentProjection, IRetriveBalanceOutputPort retriveBalanceOutputPort)
        {
            _paymentProjection = paymentProjection ?? throw new ArgumentNullException(nameof(paymentProjection));
            _retriveBalanceOutputPort = retriveBalanceOutputPort ?? throw new ArgumentNullException(nameof(retriveBalanceOutputPort));
        }

        public async Task Execute(RetriveBalanceInput input)
        {
            if (input is null)
            {
                _retriveBalanceOutputPort.BadRequest("Input is null");
                return;
            }

            var balanceAmount = await _paymentProjection.GetBalanceByStreamId(input.BeneficiaryAlias);

            if (balanceAmount is null)
            {
                _retriveBalanceOutputPort.NotFound($"Balance to {input.BeneficiaryAlias} does not exist");
                return;
            }

            _retriveBalanceOutputPort.OK(balanceAmount);
        }

    }
}
