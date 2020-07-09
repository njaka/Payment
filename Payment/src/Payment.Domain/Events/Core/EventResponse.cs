using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Events.Core
{
    public class EventResponse
    {
        public EventResponse(string eventStreamId, Guid eventId, long eventNumber, string eventName, byte[] data)
        {
            EventStreamId = eventStreamId;
            EventId = eventId;
            EventNumber = eventNumber;
            EventName = eventName;
            Data = data;
        }

        public byte[] Data { get; private set; }

        public string EventStreamId { get; private set; }

        public Guid EventId { get; private set; }

        public long EventNumber { get; private set; }

        public string EventName { get; private set; }

    }
}
