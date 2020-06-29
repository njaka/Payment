using Payment.Infrastructure.EventSourcing;
using Payment.Domain;
using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using System.Text;
using System.Text.Json;

namespace Payment.EventStore
{
    public class EventStore : IEventSourcing
    {
        private readonly IEventStoreConnection _eventSourcing;

        public EventStore(IEventStoreConnection eventSourcing)
        {
            _eventSourcing = eventSourcing;
        }

        public async Task AppendEventOnStream(Event @event, string stream)
        {
            await _eventSourcing.AppendToStreamAsync(stream, ExpectedVersion.Any, BuildEventData(@event));
        }

        private static EventData BuildEventData(Event @event)
        {
            return new EventData(
                Guid.NewGuid(),
                @event.MessageType.ToString(),
                true,
                Encoding.ASCII.GetBytes(JsonSerializer.Serialize(@event)),
                null
                );
        }
    }
}
