namespace Payment.Application.Projections
{
    using LiquidProjections;
    using Payment.Domain.Wallet;
    using System;
    using System.Collections.Generic;
    using PaymentModel = Payment.Domain.Payment;

    public static class BalanceMapExtenstions
    {
        public static IEventMap<Balance> BuildBalanceMap(this EventMapBuilder<Balance> eventMapBuilder)
        {
            return eventMapBuilder.Build(new ProjectorMap<Balance>()
            {
                Custom = (context, projector) => projector()
            });
        }

        public static IEventMap<Dictionary<Guid, PaymentModel>> BuildBalanceMap(this EventMapBuilder<Dictionary<Guid, PaymentModel>> eventMapBuilder)
        {
            return eventMapBuilder.Build(new ProjectorMap<Dictionary<Guid, PaymentModel>>()
            {
                Custom = (context, projector) => projector()
            });
        }
    }
}
