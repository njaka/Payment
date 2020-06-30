using MediatR;
using Payment.Domain;
using Payment.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Application.Events.Handlers
{
    public class OrderPaymentEventHandler : INotificationHandler<OrderPaymentCreated>,
                                            INotificationHandler<OrderPaymentStatusChanged>
    {
        private static readonly string STREAMNAME = "OrderPayment"; 

        private readonly IEventSourcingHandler _eventSourcing;

        public OrderPaymentEventHandler(IEventSourcingHandler eventSourcing)
        {
            _eventSourcing = eventSourcing;
        }
        public async Task Handle(OrderPaymentCreated notification, CancellationToken cancellationToken)
        {
            // Talk with NJaka about stream name 
            await _eventSourcing.RaiseEventAsync(notification, $"{STREAMNAME}{notification.BeneficiaryAlias}");
        }

        public async Task Handle(OrderPaymentStatusChanged notification, CancellationToken cancellationToken)
        {
            // Talk with NJaka about stream name 
            await _eventSourcing.RaiseEventAsync(notification, $"{STREAMNAME}{notification.BeneficiaryAlias}");
        }
    }
}
