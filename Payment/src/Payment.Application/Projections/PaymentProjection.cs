namespace Payment.Application.Projections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LiquidProjections;
    using Newtonsoft.Json;
    using Payment.Application.UseCases;
    using Payment.Domain;
    using Payment.Domain.Events;
    using Payment.Domain.Events.Core;
    using Payment.Domain.Wallet;
    using Payment.Infrastructure.EventSourcing;
    using PaymentModel = Payment.Domain.Payment;

    public class PaymentProjection : IPaymentProjection
    {
        private readonly IEventMap<Balance> _map;

        private readonly Dictionary<Guid, IList<PaymentModel>> _events;

        private readonly IEventSourcing _eventSourcing;
        public Balance Balance { get; } = Balance.CreateNewBalance(Money.CreateNewMoneyDollars(0), DateTime.UtcNow);

        public async Task<RetriveBalanceOutput> GetBalanceByStreamId(string streamId)
        {
            await _eventSourcing.ReadStreamEventsForward(streamId, StreamMessageReceived);

            return new RetriveBalanceOutput() {
                 Amount = Balance.Amount.Amount,
                 Currency = Balance.Amount.Currency.ToString()
            };
        }

        public PaymentProjection(IEventSourcing eventSourcing)
        {
            _eventSourcing = eventSourcing;
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
