using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidProjections;
using Newtonsoft.Json;
using Payment.Domain;
using Payment.Domain.Events;
using Payment.Domain.Events.Core;
using Payment.Domain.Wallet;
using Payment.Infrastructure.EventSourcing;

namespace Payment.Application.Projections
{
    public class PaymentProjection
    {
        private readonly IEventMap<Balance> _map;
        public Balance Balance { get; } = Balance.CreateNewBalance(Money.CreateNewMoneyDollars(0), DateTime.UtcNow);

        public Money GetBalance()
        {
            return Balance.Amount;
        }

		public PaymentProjection(IEventSourcing eventSourcing, string streamId)
		{
			var mapBuilder = new EventMapBuilder<Balance>();
			//mapBuilder.Map<Deposited>().As((deposited, balance) =>
			//{
			//	balance.Add(deposited.Amount);
			//});
			//mapBuilder.Map<Deposited>()
			//		  .When(deposited => deposited.Amount == 100)
			//		  .As((deposited, balance) =>
			//		  {
			//			  //Bonus
			//			  balance.Add(deposited.Amount);
			//		  });

			//mapBuilder.Map<Withdrawn>().As((withdrawn, balance) =>
			//{
			//	balance.Subtract(withdrawn.Amount);
			//});

			//_map = mapBuilder.Build(new ProjectorMap<Balance>()
			//{
			//	Custom = (context, projector) => projector()
			//});

			eventSourcing.ReadStreamEventsForward(streamId, StreamMessageReceived);
		}

		private Task StreamMessageReceived(EventResponse streamMessage)
		{
			var @event = DeserializeJsonEvent(streamMessage);
			_map.Handle(@event, Balance);
			return Task.CompletedTask;
		}

		private object DeserializeJsonEvent(EventResponse streamMessage)
		{
			switch (streamMessage.EventName)
			{
				case "OrderPaymentCreated":
					return JsonConvert.DeserializeObject<OrderPaymentCreated>(Encoding.ASCII.GetString(streamMessage.Data));
				case "OrderPaymentStatusChanged":
					return JsonConvert.DeserializeObject<OrderPaymentStatusChanged>(Encoding.ASCII.GetString(streamMessage.Data));
				default:
					throw new InvalidOperationException("Unknown event type.");
			}
		}
	}
}
