using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Payment.Application.UseCases;
using Payment.Domain;

namespace Payment.Application.Projections
{
    public interface IPaymentProjection
    {
        Task<RetriveBalanceOutput> GetBalanceByStreamId(string streamId);
    }
}