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

        private readonly Dictionary<Guid, PaymentModel> _events;

        private readonly IEventSourcing _eventSourcing;
        public Balance Balance { get; protected set; }

        public async Task<RetriveBalanceOutput> GetBalanceByStreamId(string streamId)
        {
            Balance = Balance.CreateNewBalance(Money.CreateNewMoneyDollars(0), DateTime.UtcNow);
            await _eventSourcing.ReadStreamEventsForward(streamId, StreamMessageReceived);

            return new RetriveBalanceOutput() {
                Amount = Balance.Amount.Amount,
                Currency = Balance.Amount.Currency.ToString()
            };
        }

        public PaymentProjection(IEventSourcing eventSourcing)
        {
            _eventSourcing = eventSourcing;
            _events = new Dictionary<Guid, PaymentModel>();
            var mapBuilder = new EventMapBuilder<Balance>();
            mapBuilder.Map<OrderPaymentCreated>().As((OrderPayment, balance) =>
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
            });

            mapBuilder.Map<OrderPaymentStatusChanged>().As((OrderPayment, balance) =>
            {

                _events[OrderPayment.AggregateId].UpdateStatus(OrderPayment.PaymentStatus.ToEnum<PaymentStatus>());
                if (_events[OrderPayment.AggregateId].Status == PaymentStatus.Succeed)
                    Balance = balance.Add(_events[OrderPayment.AggregateId].Amount).Value;

            });

            _map = mapBuilder.Build(new ProjectorMap<Balance>()
            {
                Custom = (context, projector) => projector()
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
            try
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
            catch (Exception ex)
            {

                return null;

            }

        }
    }
    public static class EnumExtension
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

    }
}
