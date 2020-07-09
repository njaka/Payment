namespace Payment.Application.Projections
{
    using LiquidProjections;
    using Newtonsoft.Json;
    using Payment.Application.UseCases;
    using Payment.Domain;
    using Payment.Domain.Events;
    using Payment.Domain.Events.Core;
    using Payment.Domain.Wallet;
    using Payment.Infrastructure.EventSourcing;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using PaymentModel = Payment.Domain.Payment;

    public class BalanceProjection : IBalanceProjection
    {
        private static readonly string STREAMNAME = "wallet-";

        private readonly IEventMap<Balance> _map;

        private readonly Dictionary<Guid, PaymentModel> _events;

        private readonly IEventSourcing _eventSourcing;
        public Balance Balance { get; protected set; }

        public async Task<RetriveBalanceOutput> GetBalanceByStreamId(string streamId)
        {
            Balance = Balance.CreateNewBalance(Money.CreateNewMoneyDollars(0), DateTime.UtcNow);
            await _eventSourcing.ReadStreamEventsForward($"{STREAMNAME}{streamId}", StreamMessageReceived);

            return new RetriveBalanceOutput()
            {
                Amount = Balance.Amount.Amount,
                Currency = Balance.Amount.Currency.ToString()
            };
        }

        public BalanceProjection(IEventSourcing eventSourcing)
        {
            _eventSourcing = eventSourcing;
            _events = new Dictionary<Guid, PaymentModel>();

            _map = CreateBalanceEventMap()
                            .BuildBalanceMap();
        }



        private EventMapBuilder<Balance> CreateBalanceEventMap()
        {
            var mapBuilder = new EventMapBuilder<Balance>();

            mapBuilder.Map<OrderPaymentPaid>().As((OrderPayment, balance) =>
            {
                AddBalance(OrderPayment, balance);

            });
            return mapBuilder;
        }

        private void AddBalance(OrderPaymentPaid orderPayment, Balance balance)
        {
            Balance = balance.Add(Money.CreateNewMoney(orderPayment.Amount, orderPayment.Currency)).Value;
        }

        private void CreatePayment(OrderPaymentCreated OrderPayment)
        {
            if (!_events.ContainsKey(OrderPayment.AggregateId))
                _events.Add(OrderPayment.AggregateId, null);

            var payment = PaymentModel
                            .CreateNewCardPayment(
                                           new Card(new CardNumber(OrderPayment.CardNumber),
                                           new ExpiryDate("06/22"),
                                           new CVV(OrderPayment.CVV)),
                                           new Money(OrderPayment.Amount, OrderPayment.Currency), OrderPayment.BeneficiaryAlias
                                        );

            _events[OrderPayment.AggregateId] = payment;
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
                    return JsonConvert.DeserializeObject<OrderPaymentPaid>(Encoding.ASCII.GetString(streamMessage.Data));
                default:
                    throw new InvalidOperationException("Unknown event type.");
            }
        }
    }
}
