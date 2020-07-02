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
using PaymentModel = Payment.Domain.Payment;
namespace Payment.Application.Projections
{
    public class PaymentProjection
    {
        private readonly IEventMap<Balance> _map;

        private readonly Dictionary<Guid, IList<PaymentModel>> _events;


        public Balance Balance { get; } = Balance.CreateNewBalance(Money.CreateNewMoneyDollars(0), DateTime.UtcNow);

        public Money GetBalance()
        {
            return Balance.Amount;
        }

        public PaymentProjection(IEventSourcing eventSourcing, string streamId)
        {
            _events = new Dictionary<Guid, IList<PaymentModel>>();
            var mapBuilder = new EventMapBuilder<Balance>();
            mapBuilder.Map<OrderPaymentCreated>().As((OrderPayment, balance) =>
            {
                if (!_events.ContainsKey(OrderPayment.AggregateId))
                    _events.Add(OrderPayment.AggregateId, new List<PaymentModel>());

                var payment = PaymentModel
                                .CreateNewCardPayment(
                                               new Card(new CardNumber(OrderPayment.CardNumber), 
                                               new ExpiryDate(OrderPayment.ExpirationDate.ToString()), 
                                               new CVV(OrderPayment.CVV)),
                                               new Money(OrderPayment.Amount, OrderPayment.Currency), OrderPayment.BeneficiaryAlias
                                            );

                _events[OrderPayment.AggregateId].Add(payment);
            });

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
