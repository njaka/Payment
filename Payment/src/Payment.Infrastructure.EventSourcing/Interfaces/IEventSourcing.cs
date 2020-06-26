using Payment.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure.EventSourcing
{
    public interface IEventSourcing
    {
        Task AppendEventOnStream(Event @event, string stream);
    }
}
