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
        public Task Handle(OrderPaymentOpened notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(OrderPaymentPaid notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
