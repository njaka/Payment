﻿using Payment.Domain;
using Payment.Domain.Events.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure.EventSourcing
{
    public interface IEventSourcing
    {
        Task AppendEventOnStreamAsync<T>(T @event, string stream) where T : Event;
        Task ReadStreamEventsForward(string stream, StreamMessageReceived streamMessageReceived );

        Task SubscribeToAllAsync(StreamMessageReceived streamMessageReceived);

        Task ReadAllEventsForward(StreamMessageReceived streamMessageReceived);

        public delegate Task StreamMessageReceived(EventResponse streamMessage);
    }
}
