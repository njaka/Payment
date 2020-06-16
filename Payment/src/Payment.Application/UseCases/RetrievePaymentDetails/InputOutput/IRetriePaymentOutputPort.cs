
namespace Payment.Application.UseCases
{
    using Payment.Domain;
    public interface IRetriePaymentOutputPort : IOutputOK<RetrievePaymentDetailOutput>, IOutPutNotFound, IOutputBadRequest
    {
    }
}
