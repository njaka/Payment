using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidProjections;
using Payment.Domain.Wallet;

namespace Payment.Application.Projections
{
    public class PaymentProjection
    {
        private readonly IEventMap<Balance> _map;
        //public Balance Balance { get; } = new Balance(0, DateTime.UtcNow);
    }
}
