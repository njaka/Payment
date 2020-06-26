using MediatR;
using Payment.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure.EventSourcing
{
    public sealed class EventStore : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventSourcing _eventSourcing;

        public EventStore(IEventSourcing eventSourcing, IMediator mediator)
        {
            _eventSourcing = eventSourcing;
            _mediator = mediator;
        }

        public async Task<object> SendCommand<T>(T command)
        {
            return await _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event, string stream) where T : Event
        {
            return _eventSourcing.AppendEventOnStream(@event, stream);
        }
    }
}