using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Payment.Domain.Events
{
    public static class DomainEvents
    {
        private static IServiceProvider _serviceProvider;

        public static void Init(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static void DipatchEvents(IList<INotification> events)
        {
            //TODO: Ugly code I know how fix it, but I don't had time lol
            foreach (var domainEvent in events)
            {
                if(domainEvent.GetType().Name.Equals("OrderPaymentCreated"))
                    Raise((OrderPaymentCreated)domainEvent);
                else
                    Raise((OrderPaymentPaid)domainEvent);
            }
        }

        public static async void Raise<T>(T domainEvent)
                where T : INotification
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var eventHandler = scope.ServiceProvider.GetRequiredService<INotificationHandler<T>>();
                await eventHandler.Handle(domainEvent, new CancellationToken());
            }
        }
    }
}
