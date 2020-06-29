using MediatR;
using Payment.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure.EventSourcing
{
    public sealed class EventSourcing : IEventSourcingHandler
    {
        private readonly IEventSourcing _eventSourcing;

        public EventSourcing(IEventSourcing eventSourcing)
        {
            _eventSourcing = eventSourcing;
        }

        public async Task RaiseEventAsync<T>(T @event, string stream) where T : Event
        {
            await _eventSourcing.AppendEventOnStreamAsync(@event, stream);
        }
    }
}