using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.UseCases
{
    public interface IRetriveBalanceOutputPort : IOutputOK<RetriveBalanceOutput>, IOutPutNotFound, IOutputBadRequest
    {
    }
}
