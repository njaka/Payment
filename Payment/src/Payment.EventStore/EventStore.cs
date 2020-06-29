﻿using Payment.Infrastructure.EventSourcing;
using Payment.Domain;
using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using System.Text;
using System.Text.Json;
using System.Net;
using EventStore.ClientAPI.SystemData;

namespace Payment.EventStore
{
    public class EventStore : IEventSourcing
    {
        private IEventStoreConnection _eventSourcing;

        public EventStore(IEventStoreConnection eventSourcing)
        {
            _eventSourcing = eventSourcing;
        }

        public async Task AppendEventOnStreamAsync<T>(T @event, string stream) where T : Event
        {
            await _eventSourcing.ConnectAsync();
            await _eventSourcing.AppendToStreamAsync(stream, ExpectedVersion.Any, BuildEventData(@event)).ConfigureAwait(false);
        }

        private static EventData BuildEventData<T>(T @event) where T : Event
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
