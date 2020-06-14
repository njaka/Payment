
namespace Payment.Application.UseCases
{
    using Payment.Domain;
    public interface IRetriePaymentOutputPort : IOutputOK<Payment>, IOutPutNotFound, IOutputBadRequest
    {
    }
}
