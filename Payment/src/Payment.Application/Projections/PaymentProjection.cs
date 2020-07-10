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
    using Payment.Domain.DomainServices;
    using Payment.Domain.Events;
    using Payment.Domain.Events.Core;
    using Payment.Domain.Wallet;
    using Payment.Infrastructure.EventSourcing;
    using PaymentModel = Payment.Domain.Payment;

    public class PaymentProjection : IPaymentProjection
    {
        private static readonly string STREAMNAME = "wallet-";

        private readonly IEventMap<Dictionary<Guid, PaymentModel>> _map;

        private Dictionary<Guid, PaymentModel> _events;

        private readonly IEventSourcing _eventSourcing;


        public async Task<PaymentModel> GetById(Guid paymentId)
        {
            _events = new Dictionary<Guid, PaymentModel>();
            await _eventSourcing.ReadAllEventsForward(StreamMessageReceived);

            return _events[paymentId];
        }
    

        public PaymentProjection(IEventSourcing eventSourcing)
        {
            _eventSourcing = eventSourcing;
            _events = new Dictionary<Guid, PaymentModel>();

            _map = CreatePaymentEventMap()
                            .BuildBalanceMap();
        }



        private EventMapBuilder<Dictionary<Guid, PaymentModel>> CreatePaymentEventMap()
        {
            var mapBuilder = new EventMapBuilder<Dictionary<Guid, PaymentModel>>();
            mapBuilder.Map<OrderPaymentCreated>().As((OrderPayment, events) =>
            {
                CreatePayment(OrderPayment);
            });

            mapBuilder.Map<OrderPaymentPaid>().As((OrderPayment, events) =>
            {
                UpdateStatus(OrderPayment);

            });
            return mapBuilder;
        }

        private void UpdateStatus(OrderPaymentPaid OrderPayment)
        {
            _events[OrderPayment.AggregateId].Paid();
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
                                           new Money(OrderPayment.Amount, OrderPayment.Currency, new CurrencyLookup()), OrderPayment.BeneficiaryAlias
                                        );

            _events[OrderPayment.AggregateId] = payment;
        }

        private Task StreamMessageReceived(EventResponse streamMessage)
        {
            var @event = DeserializeJsonEvent(streamMessage);
            
            _map.Handle(@event, _events);
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
                    return null;
            }
        }
    }
}
