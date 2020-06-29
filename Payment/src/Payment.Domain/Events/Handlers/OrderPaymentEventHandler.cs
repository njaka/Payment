using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Domain.Events.Handlers
{
    public class OrderPaymentEventHandler : INotificationHandler<OrderPaymentOpened>,
                                            INotificationHandler<OrderPaymentPaid>
    {
        private static readonly string STREAMNAME = "OrderPayment"; 

        private readonly IEventSourcingHandler _eventSourcing;

        public OrderPaymentEventHandler(IEventSourcingHandler eventSourcing)
        {
            _eventSourcing = eventSourcing;
        }
        public async Task Handle(OrderPaymentOpened notification, CancellationToken cancellationToken)
        {
            // Talk with NJaka about stream name 
            await _eventSourcing.RaiseEvent(notification, STREAMNAME);
        }

        public async Task Handle(OrderPaymentPaid notification, CancellationToken cancellationToken)
        {
            // Talk with NJaka about stream name 
            await _eventSourcing.RaiseEvent(notification, STREAMNAME);
        }
    }
}
