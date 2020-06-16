namespace Payment.Application.UseCases
{
    public interface IProcessPaymentOutputPort : IOutputOK<ProcessPaymentOutput>, IOutPutNotFound, IOutputBadRequest
    {

    }
}
