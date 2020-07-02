using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.UseCases
{
    using Payment.Domain;
    public class RetriveBalanceInput
    {
        public String BeneficiaryAlias { get; set; }
    }
}
