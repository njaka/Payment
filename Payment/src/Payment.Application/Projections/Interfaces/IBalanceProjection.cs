using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Payment.Application.UseCases;
using Payment.Domain;

namespace Payment.Application.Projections
{
    public interface IBalanceProjection
    {
        Task<RetriveBalanceOutput> GetBalanceByStreamId(string streamId);
    }
}