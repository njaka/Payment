using Payment.Infrastructure.EventSourcing;
using Payment.Domain;
using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using System.Text;
using System.Net;
using EventStore.ClientAPI.SystemData;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Payment.Domain.Events.Core;
using static Payment.Infrastructure.EventSourcing.IEventSourcing;

namespace Payment.EventStore
{
    public class EventStore : IEventSourcing, IDisposable
    {
        private IEventStoreConnection _eventSourcing;

        public EventStore(IEventStoreConnection eventSourcing)
        {
            _eventSourcing = eventSourcing;
            _eventSourcing.ConnectAsync().Wait();
        }

        public async Task AppendEventOnStreamAsync<T>(T @event, string stream) where T : Event
        {
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

        public void Dispose()
        {
            _eventSourcing.Close();
            _eventSourcing = null;
        }

        public async Task ReadStreamEventsForward(string stream, StreamMessageReceived streamMessageReceived)
        {
            StreamEventsSlice currentSlice = null;
            long nextSliceStart = StreamPosition.Start;
            do
            {
                currentSlice = await _eventSourcing.ReadStreamEventsForwardAsync(stream, nextSliceStart, 100, false)
                                .ConfigureAwait(false);
                nextSliceStart = currentSlice.NextEventNumber;
                foreach (var @event in currentSlice.Events)
                {
                    await streamMessageReceived(new EventResponse(
                                                    @event.Event.EventStreamId,
                                                    @event.Event.EventId,
                                                    @event.Event.EventNumber,
                                                    @event.Event.EventType,
                                                    @event.Event.Data
                                                ));
                }

            } while (!currentSlice.IsEndOfStream);
        }

        
        public async Task ReadAllEventsForward(StreamMessageReceived streamMessageReceived)
        {
            AllEventsSlice currentSlice;
            var nextSliceStart = Position.Start;
            do
            {
                currentSlice = await _eventSourcing.ReadAllEventsForwardAsync(nextSliceStart, 100, false)
                                .ConfigureAwait(false);

                nextSliceStart = currentSlice.NextPosition;

                foreach (var @event in currentSlice.Events)
                {
                    await streamMessageReceived(new EventResponse(
                                                    @event.Event.EventStreamId,
                                                    @event.Event.EventId,
                                                    @event.Event.EventNumber,
                                                    @event.Event.EventType,
                                                    @event.Event.Data
                                                ));
                }

            } while (!currentSlice.IsEndOfStream);
        }

        public async Task SubscribeToAllAsync(StreamMessageReceived streamMessageReceived)
        {
            // TODO: CARLOS
            await Task.FromResult(true);
        }
    }
}
